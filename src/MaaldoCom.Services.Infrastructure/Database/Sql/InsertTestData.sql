DECLARE @testAlbum1 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 1','test-media-album-1','test description 1');
SET @testAlbum1 = SCOPE_IDENTITY()

DECLARE @testAlbum2 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 2','test-media-album-2','test description 2');
SET @testAlbum2 = SCOPE_IDENTITY()

DECLARE @testAlbum3 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 3','test-media-album-3','test description 3');
SET @testAlbum3 = SCOPE_IDENTITY()

DECLARE @testAlbum4 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 4','test-media-album-4','test description 4');
SET @testAlbum4 = SCOPE_IDENTITY()

DECLARE @testAlbum5 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 5','test-media-album-5','test description 5');
SET @testAlbum5 = SCOPE_IDENTITY()

/*
DECLARE @Counter INT;
SET @Counter = 1;

WHILE (@Counter <= 10)
    BEGIN
        
        
        --PRINT 'The current counter value is = ' + CONVERT(VARCHAR, @Counter);
        --SET @Counter = @Counter + 1; -- Increment the counter
    END
*/