using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Api.Application.Commands.MediaAlbums;

public sealed record CreateMediaAlbumCommand(ClaimsPrincipal User, MediaAlbumDto MediaAlbum) : ICommand<MediaAlbumDto>;

internal sealed class CreateMediaAlbumCommandHandler(IMaaldoComDbContext maaldoComDbContext, ICacheManager cacheManager)
    : ICommandHandler<CreateMediaAlbumCommand, MediaAlbumDto>
{
    public async Task<Result<MediaAlbumDto>> HandleAsync(CreateMediaAlbumCommand command, CancellationToken ct)
    {
        command.MediaAlbum.Active = true;
        foreach (var media in command.MediaAlbum.Media) { media.Active = true; }

        // collect all unique tag names from album-level and media-level tags
        var allTagNamesInRequest = command.MediaAlbum.Tags
            .Concat(command.MediaAlbum.Media.SelectMany(m => m.Tags))
            .Select(t => t.Name!.ToLower())
            .Distinct()
            .ToList();

        // resolve existing tags from the database
        var matchingTagsInDb = await maaldoComDbContext.Tags
            .Where(t => allTagNamesInRequest.Contains(t.Name!.ToLower()))
            .ToListAsync(ct);

        // create new Tag entities for names not found in the DB
        var existingTagNamesInDb = matchingTagsInDb.Select(t => t.Name!.ToLowerInvariant()).ToHashSet();
        var newTagsToCreate = allTagNamesInRequest
            .Where(name => !existingTagNamesInDb.Contains(name))
            .Select(name => new Tag { Name = name })
            .ToList();

        if (newTagsToCreate.Count > 0) { await maaldoComDbContext.Tags.AddRangeAsync(newTagsToCreate, ct); }

        // build lookup dictionary keyed by lowercase name
        var tagLookup = matchingTagsInDb.Concat(newTagsToCreate).ToDictionary(t => t.Name!.ToLowerInvariant());

        var entity = command.MediaAlbum.ToEntity();

        // replace album-level tags with resolved references
        entity.MediaAlbumTags = command.MediaAlbum.Tags
            .Select(t => new MediaAlbumTag { Tag = tagLookup[t.Name!.ToLowerInvariant()] })
            .ToList();

        // replace media-level tags with resolved references
        foreach (var (mediaEntity, mediaDto) in entity.Media.Zip(command.MediaAlbum.Media))
        {
            mediaEntity.MediaTags = mediaDto.Tags
                .Select(t => new MediaTag { Tag = tagLookup[t.Name!.ToLowerInvariant()] })
                .ToList();
        }

        await maaldoComDbContext.MediaAlbums.AddAsync(entity, ct);
        await maaldoComDbContext.SaveChangesAsync(command.User, ct);

        await cacheManager.InvalidateCache(CacheKeys.MediaAlbumList, ct);
        await cacheManager.InvalidateCache(CacheKeys.TagList, ct);

        return Result.Ok(entity.ToDto());
    }
}

public class CreateMediaAlbumCommandValidator : AbstractValidator<CreateMediaAlbumCommand>
{
    public CreateMediaAlbumCommandValidator(IMaaldoComDbContext maaldoComDbContext)
    {
        RuleFor(x => x.MediaAlbum)
            .NotNull()
            .WithMessage("Media album cannot be null");
        RuleFor(x => x.MediaAlbum).SetValidator(new CreateMediaAlbumValidator(maaldoComDbContext));
    }
}
