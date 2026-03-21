namespace MaaldoCom.Api.Extensions;

public static class ModelMapperExtensions
{
    public static IEnumerable<GetMediaAlbumResponse> ToGetModels(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetMediaResponse> ToGetModels(this IEnumerable<MediaDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetTagResponse> ToGetModels(this IEnumerable<TagDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetKnowledgeResponse> ToGetModels(this IEnumerable<KnowledgeDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetWritingResponse> ToGetModels(this IEnumerable<WritingDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetMediaAlbumDetailResponse> ToDetailModels(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToDetailModel()).ToList();
    }

        extension<TModel>(TModel model) where TModel : BaseModel
    {
        private TModel MapToBaseModel<TDto>(TDto dto) where TDto : BaseDto
        {
            model.Id = dto.Id;

            return model;
        }
    }

    public static GetMediaAlbumResponse ToGetModel(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaAlbumResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.Slug = dto.Slug;
        model.Created = dto.Created;
        model.Tags = dto.Tags.Select(t => t.Name!).ToList();
        model.DefaultMediaId = dto.DefaultMediaId;

        return model;
    }

    public static GetMediaResponse ToGetModel(this MediaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaResponse().MapToBaseModel(dto);

        model.FileName = dto.FileName;
        model.Description = dto.Description;
        model.SizeInBytes = dto.SizeInBytes;
        model.ContentType = dto.ContentType;
        model.Tags = dto.Tags?.Select(t => t.Name!).ToList()!;

        model.MediaAlbumId = dto.MediaAlbumId;

        return model;
    }

    public static GetTagResponse ToGetModel(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetTagResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.Count = dto.Count;

        return model;
    }

    public static GetKnowledgeResponse ToGetModel(this KnowledgeDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetKnowledgeResponse().MapToBaseModel(dto);

        model.Title = dto.Title;
        model.Quote = dto.Quote;

        return model;
    }

    public static GetWritingResponse ToGetModel(this WritingDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetWritingResponse().MapToBaseModel(dto);

        model.Title = dto.Title;
        model.Slug = dto.Slug;
        model.Blurb = dto.Blurb;
        model.Created = dto.Created;
        model.Tags = dto.Tags.Select(t => t.Name!).ToList();

        return model;
    }

    public static GetCommentResponse ToGetModel(this CommentDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetCommentResponse
        {
            Author = dto.Author,
            Body = dto.Body,
            Created = dto.Created
        };

        return model;
    }

    public static GetMediaAlbumDetailResponse ToDetailModel(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaAlbumDetailResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.Slug = dto.Slug;
        model.Created = dto.Created;
        model.Description = dto.Description;
        model.Active = dto.Active;
        model.Media = dto.Media.Select(m => m.ToGetModel()).ToList();
        model.Tags = dto.Tags.Select(m => m.Name!).ToList();
        model.DefaultMediaId = dto.DefaultMediaId;
        model.Comments = dto.Comments.Select(c => c.ToGetModel()).ToList();

        return model;
    }

    public static GetTagDetailResponse ToDetailModel(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetTagDetailResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.Count = dto.Count;
        model.MediaAlbums = dto.MediaAlbums.Select(ma => new GetMediaAlbumTagResponse
        {
            Name = ma.Name,
            MediaAlbumId = ma.Id,
            Slug = ma.Slug,
            Href = UrlMaker.GetMediaAlbumUrl(ma.Id)
        });
        model.Media = dto.Media.Select(m => new GetMediaTagResponse
        {
            Name = m.FileName,
            MediaId = m.Id,
            MediaAlbumId = m.MediaAlbumId,
            MediaAlbumName = m.MediaAlbumName,
            MediaAlbumSlug = m.MediaAlbumSlug,
            Href = UrlMaker.GetMediaUrl(m.MediaAlbumId, m.Id)
        });
        model.Writings = dto.Writings.Select(w => new GetWritingTagResponse
        {
            Title = w.Title,
            WritingId = w.Id,
            Slug = w.Slug,
            Href = UrlMaker.GetWritingUrl(w.Id)
        });

        return model;
    }

    public static GetWritingDetailResponse ToDetailModel(this WritingDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetWritingDetailResponse().MapToBaseModel(dto);

        model.Title = dto.Title;
        model.Slug = dto.Slug;
        model.Blurb = dto.Blurb;
        model.Created = dto.Created;
        model.Tags = dto.Tags.Select(t => t.Name!).ToList();
        model.Comments = dto.Comments.Select(c => c.ToGetModel()).ToList();
        model.Body = dto.Body!;

        return model;
    }

    public static PostMediaAlbumResponse ToPostModel(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new PostMediaAlbumResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.Slug = dto.Slug;
        model.Created = dto.Created;

        if (dto.Tags != null)
        {
            model.Tags = dto.Tags.Select(m => m.Name!).ToList();
        }

        return model;
    }
}
