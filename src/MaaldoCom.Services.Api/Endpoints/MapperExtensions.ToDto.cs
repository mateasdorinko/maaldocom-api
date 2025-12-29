using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums;
using MaaldoCom.Services.Api.Endpoints.Knowledge;

namespace MaaldoCom.Services.Api.Endpoints;

public static partial class MapperExtensions
{
    public static MediaAlbumDto ToDto(this GetMediaAlbumResponse model)
    {
        return new MediaAlbumDto
        {
            Id =  model.Id,
            Name = model.Name,
            UrlFriendlyName = model.UrlFriendlyName,
            Created = model.Created,
            //Tags = model.Tags.Select(m => m.)
        };
    }
    
    public static MediaAlbumDto ToDto(this GetMediaAlbumDetailResponse model)
    {
        var dto = 
        
        return new MediaAlbumDto
        {
            Id =  model.Id,
            Name = model.Name,
            UrlFriendlyName = model.UrlFriendlyName,
            Created = model.Created,
            Description = model.Description,
            Active = model.Active,
            Media = ,
            Tags = 
        };
    }
}