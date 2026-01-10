using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Api.Endpoints.Tags.Models;

namespace MaaldoCom.Services.Api.Extensions;

public static partial class MapperExtensions
{
    extension<TModel>(TModel model) where TModel : BaseModel
    {
        private TModel MapToBaseModel<TDto>(TDto dto) where TDto : BaseDto
        {
            model.Id = dto.Id;

            return model;
        }
    }

    public static GetMediaAlbumResponse ToModel(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaAlbumResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.UrlFriendlyName = dto.UrlFriendlyName;
        model.Created = dto.Created;
        model.Tags = dto.Tags.Select(m => m.Name!).ToList();

        return model;
    }

    public static GetMediaResponse ToModel(this MediaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaResponse().MapToBaseModel(dto);

        model.FileName = dto.FileName;
        model.Description = dto.Description;
        model.SizeInBytes = dto.SizeInBytes;
        model.Tags = dto.Tags?.Select(m => m.Name!).ToList()!;
        model.BlobUrl = dto.BlobUrl;

        model.MediaAlbumId = dto.MediaAlbumId;

        return model;
    }

    public static GetTagResponse ToModel(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetTagResponse().MapToBaseModel(dto);

        model.Name = dto.Name;

        return model;
    }

    public static GetKnowledgeResponse ToModel(this KnowledgeDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetKnowledgeResponse().MapToBaseModel(dto);

        model.Title = dto.Title;
        model.Quote = dto.Quote;

        return model;
    }
}