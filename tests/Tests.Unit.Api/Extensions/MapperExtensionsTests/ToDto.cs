namespace Tests.Unit.Api.Extensions.MapperExtensionsTests;

public class ToDto
{
    [Fact]
    public void ToDto_FromGetMediaAlbumResponseModel_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetMediaAlbumResponse
        {
            Id = Guid.NewGuid(),
            Name = "Sample Album",
            UrlFriendlyName = "sample-album",
            Created = DateTime.UtcNow,
            Tags = new List<string> { "SampleTag" }
        };
        
        // act
        var dto = model.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(model.Id);
        dto.Name.ShouldBeEquivalentTo(model.Name);
        dto.UrlFriendlyName.ShouldBeEquivalentTo(model.UrlFriendlyName);
        dto.Created.ShouldBeEquivalentTo(model.Created);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags[0].Name.ShouldBeEquivalentTo("SampleTag");
    }
    
    [Fact]
    public void ToDto_FromNullGetMediaAlbumResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetMediaAlbumResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_FromGetMediaAlbumDetailResponse_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetMediaAlbumDetailResponse
        {
            Id = Guid.NewGuid(),
            Name = "Sample Album",
            UrlFriendlyName = "sample-album",
            Created = DateTime.UtcNow,
            Description = "This is a sample media album.",
            Active = true,
            Tags = new List<string> { "SampleTag" },
            Media = new List<GetMediaResponse>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "sample.jpg",
                    Description = "This is a sample media file.",
                    Tags = new List<string> { "SampleTag" }
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
        dto.Description.ShouldBeEquivalentTo(model.Description);
        dto.Active.ShouldBeEquivalentTo(model.Active);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags[0].Name.ShouldBeEquivalentTo("SampleTag");
        dto.Media.Count.ShouldBe(1);
        dto.Media[0].Id.ShouldBeEquivalentTo(model.Media.First().Id);
    }

    [Fact]
    public void ToDto_FromNullGetMediaAlbumDetailResponse_ThrowsArgumentNullException()
    {
        // arrange
        GetMediaAlbumDetailResponse? model = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_FromGetMediaResponseModel_MapsAllPropertiesCorrectly()
    {
        // arrange
        var model = new GetMediaResponse
        {
            Id = Guid.NewGuid(),
            FileName = "sample.jpg",
            Description = "This is a sample media file.",
            Tags = new List<string> { "SampleTag" }
        };
        
        // act
        var dto = model.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(model.Id);
        dto.FileName.ShouldBeEquivalentTo(model.FileName);
        dto.Description.ShouldBeEquivalentTo(model.Description);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags[0].Name.ShouldBeEquivalentTo("SampleTag");
    }
    
    [Fact]
    public void ToDto_FromNullGetMediaResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetMediaResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_FromGetTagResponseModel_MapsAllPropertiesCorrectly()
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
    public void ToDto_FromNullGetTagResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetTagResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }

    [Fact]
    public void ToDto_FromGetKnowledgeResponseModel_MapsAllPropertiesCorrectly()
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
    public void ToDto_FromNullGetKnowledgeResponseModel_ThrowsArgumentNullException()
    {
        // arrange
        GetKnowledgeResponse? model = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => model!.ToDto());
    }
}
