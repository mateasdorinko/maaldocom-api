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

    extension(MediaAlbumDto dto)
    {
        public GetMediaAlbumResponse ToModel()
        {
            ArgumentNullException.ThrowIfNull(dto);

            var model = new GetMediaAlbumResponse().MapToBaseModel(dto);

            model.Name = dto.Name;
            model.UrlFriendlyName = dto.UrlFriendlyName;
            model.Created = dto.Created;
            model.Tags = dto.Tags.Select(m => m.Name!).ToList();

            return model;
        }

        public GetMediaAlbumDetailResponse ToDetailModel()
        {
            ArgumentNullException.ThrowIfNull(dto);

            var model = new GetMediaAlbumDetailResponse().MapToBaseModel(dto);

            model.Name = dto.Name;
            model.UrlFriendlyName = dto.UrlFriendlyName;
            model.Created = dto.Created;
            model.Description = dto.Description;
            model.Active = dto.Active;
            model.Media = dto.Media.Select(m => m.ToModel()).ToList();
            model.Tags = dto.Tags.Select(m => m.Name!).ToList();

            return model;
        }
    }

    public static GetMediaResponse ToModel(this MediaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaResponse().MapToBaseModel(dto);

        model.FileName = dto.FileName;
        model.Description = dto.Description;
        model.SizeInBytes = dto.SizeInBytes;
        model.Tags = dto.Tags?.Select(m => m.Name!).ToList()!;

        model.MediaAlbumId = dto.MediaAlbumId;

        return model;
    }

    extension(TagDto dto)
    {
        public GetTagResponse ToModel()
        {
            ArgumentNullException.ThrowIfNull(dto);

            var model = new GetTagResponse().MapToBaseModel(dto);

            model.Name = dto.Name;

            return model;
        }

        public GetTagDetailResponse ToDetailModel()
        {
            ArgumentNullException.ThrowIfNull(dto);

            var model = new GetTagDetailResponse().MapToBaseModel(dto);

            model.Name = dto.Name;
            model.MediaAlbums = dto.MediaAlbums.Select(ma => new GetMediaAlbumTagResponse
            {
                Name = ma.Name,
                Href = UrlMaker.GetMediaAlbumUrl(ma.Id)
            });
            model.Media = dto.Media.Select(m => new GetMediaTagResponse
            {
                Name = m.FileName,
                MediaAlbumName = m.MediaAlbumName,
                Href = UrlMaker.GetMediaUrl(m.MediaAlbumId, m.Id)
            });

            return model;
        }
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