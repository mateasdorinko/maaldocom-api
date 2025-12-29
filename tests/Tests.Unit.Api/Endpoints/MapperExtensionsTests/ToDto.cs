namespace Tests.Unit.Api.Endpoints.MapperExtensionsTests;

public class ToDto
{
    [Fact]
    public void ToDto_MappingGetMediaAlbumResponseModel_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetMediaAlbumResponse
        {
            Id = Guid.NewGuid(),
            Name = "Sample Album",
            UrlFriendlyName = "sample-album",
            Created = DateTime.UtcNow,
            Tags = new List<GetTagResponse>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "SampleTag"
                }
            }
        };
        
        // act
        var dto = model.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(model.Id);
        dto.Name.ShouldBeEquivalentTo(model.Name);
        dto.UrlFriendlyName.ShouldBeEquivalentTo(model.UrlFriendlyName);
        dto.Created.ShouldBeEquivalentTo(model.Created);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags.First().Name.ShouldBeEquivalentTo("SampleTag");
    }
    
    [Fact]
    public void ToDto_NullGetMediaAlbumResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetMediaAlbumResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_MappingGetMediaResponseModel_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetMediaResponse
        {
            Id = Guid.NewGuid(),
            FileName = "sample.jpg",
            Description = "This is a sample media file.",
            Tags = new List<GetTagResponse>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "SampleTag"
                }
            }
        };
        
        // act
        var dto = model.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(model.Id);
        dto.FileName.ShouldBeEquivalentTo(model.FileName);
        dto.Description.ShouldBeEquivalentTo(model.Description);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags.First().Name.ShouldBeEquivalentTo("SampleTag");
    }
    
    [Fact]
    public void ToDto_NullGetMediaResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetMediaResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_MappingGetTagResponseModel_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetTagResponse
        {
            Id = Guid.NewGuid(),
            Name = "SampleTag"
        };
        
        // act
        var dto = model.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(model.Id);
        dto.Name.ShouldBeEquivalentTo(model.Name);
    }
    
    [Fact]
    public void ToDto_NullGetTagResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetTagResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_MappingGetKnowledgeResponseModel_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetKnowledgeResponse()
        {
            Id = Guid.NewGuid(),
            Title = "Sample Knowledge",
            Quote = "This is some sample knowledge content."
        };
        
        // act
        var dto = model.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(model.Id);
        dto.Title.ShouldBeEquivalentTo(model.Title);
        dto.Quote.ShouldBeEquivalentTo(model.Quote);
    }
    
    [Fact]
    public void ToDto_NullGetKnowledgeResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetKnowledgeResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }
}