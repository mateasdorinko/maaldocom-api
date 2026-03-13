namespace MaaldoCom.Api.Extensions;

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
}
