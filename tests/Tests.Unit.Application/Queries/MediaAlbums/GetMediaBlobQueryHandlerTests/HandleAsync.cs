using MaaldoCom.Api.Application.Blobs;
using MaaldoCom.Api.Application.Queries.MediaAlbums;

namespace Tests.Unit.Application.Queries.MediaAlbums.GetMediaBlobQueryHandlerTests;

public class HandleAsync
{
    private const string ContainerName = "media-albums";

    private static (ICacheManager cacheManager, IBlobsProvider blobsProvider, GetMediaBlobQueryHandler handler) CreateHandler()
    {
        var cacheManager = A.Fake<ICacheManager>();
        var blobsProvider = A.Fake<IBlobsProvider>();
        var handler = new GetMediaBlobQueryHandler(cacheManager, blobsProvider);
        return (cacheManager, blobsProvider, handler);
    }

    [Fact]
    public async Task HandleAsync_WhenMediaAlbumNotFoundInCache_ReturnsNotFound()
    {
        // arrange
        var (cacheManager, _, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var query = new GetMediaBlobQuery(Guid.NewGuid(), Guid.NewGuid(), "original");

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(query.MediaAlbumId, ct))
            .Returns(default(MediaAlbumDto));

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<BlobNotFoundError>();
    }

    [Fact]
    public async Task HandleAsync_WhenMediaNotFoundInAlbum_ReturnsNotFound()
    {
        // arrange
        var (cacheManager, _, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var mediaAlbumId = Guid.NewGuid();
        var query = new GetMediaBlobQuery(mediaAlbumId, Guid.NewGuid(), "original");

        var album = new MediaAlbumDto
        {
            Id = mediaAlbumId,
            UrlFriendlyName = "my-album",
            Media = [new MediaDto { Id = Guid.NewGuid(), FileName = "photo.jpg" }]
        };

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(mediaAlbumId, ct)).Returns(album);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<BlobNotFoundError>();
    }

    [Fact]
    public async Task HandleAsync_WithOriginalMediaType_FetchesOriginalBlob()
    {
        // arrange
        var (cacheManager, blobsProvider, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var mediaId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var query = new GetMediaBlobQuery(albumId, mediaId, "original");

        var media = new MediaDto { Id = mediaId, FileName = "photo.jpg" };
        var album = new MediaAlbumDto { Id = albumId, UrlFriendlyName = "my-album", Media = [media] };
        var blobDto = new MediaDto { FileName = "photo.jpg", Stream = Stream.Null };

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(albumId, ct)).Returns(album);
        A.CallTo(() => blobsProvider.GetBlobAsync(ContainerName, "my-album/original/photo.jpg", ct))
            .Returns(blobDto);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(blobDto);
    }

    [Fact]
    public async Task HandleAsync_WithViewerMediaType_FetchesViewerBlob()
    {
        // arrange
        var (cacheManager, blobsProvider, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var mediaId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var query = new GetMediaBlobQuery(albumId, mediaId, "viewer");

        var media = new MediaDto { Id = mediaId, FileName = "photo.jpg" };
        var album = new MediaAlbumDto { Id = albumId, UrlFriendlyName = "my-album", Media = [media] };
        var blobDto = new MediaDto { FileName = "viewer-photo.jpg", Stream = Stream.Null };

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(albumId, ct)).Returns(album);
        A.CallTo(() => blobsProvider.GetBlobAsync(ContainerName, "my-album/viewer/viewer-photo.jpg", ct))
            .Returns(blobDto);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(blobDto);
    }

    [Fact]
    public async Task HandleAsync_WithThumbMediaType_FetchesThumbnailBlob()
    {
        // arrange
        var (cacheManager, blobsProvider, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var mediaId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var query = new GetMediaBlobQuery(albumId, mediaId, "thumb");

        var media = new MediaDto { Id = mediaId, FileName = "photo.jpg" };
        var album = new MediaAlbumDto { Id = albumId, UrlFriendlyName = "my-album", Media = [media] };
        var blobDto = new MediaDto { FileName = "thumb-photo.jpg", Stream = Stream.Null };

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(albumId, ct)).Returns(album);
        A.CallTo(() => blobsProvider.GetBlobAsync(ContainerName, "my-album/thumb/thumb-photo.jpg", ct))
            .Returns(blobDto);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(blobDto);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownMediaType_ReturnsNotFound()
    {
        // arrange
        var (cacheManager, _, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var mediaId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var query = new GetMediaBlobQuery(albumId, mediaId, "unknown");

        var media = new MediaDto { Id = mediaId, FileName = "photo.jpg" };
        var album = new MediaAlbumDto { Id = albumId, UrlFriendlyName = "my-album", Media = [media] };

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(albumId, ct)).Returns(album);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<BlobNotFoundError>();
    }

    [Fact]
    public async Task HandleAsync_WhenBlobProviderReturnsNull_ReturnsNotFound()
    {
        // arrange
        var (cacheManager, blobsProvider, handler) = CreateHandler();
        var ct = CancellationToken.None;
        var mediaId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var query = new GetMediaBlobQuery(albumId, mediaId, "original");

        var media = new MediaDto { Id = mediaId, FileName = "photo.jpg" };
        var album = new MediaAlbumDto { Id = albumId, UrlFriendlyName = "my-album", Media = [media] };

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(albumId, ct)).Returns(album);
        A.CallTo(() => blobsProvider.GetBlobAsync(ContainerName, A<string>._, ct))
            .Returns(default(MediaDto));

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<BlobNotFoundError>();
    }
}
