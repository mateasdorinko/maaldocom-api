DECLARE @testAlbum1 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 1','test-media-album-1','test description 1');
SET @testAlbum1 = (select Id from MediaAlbums where UrlFriendlyName = 'test-media-album-1')

DECLARE @testAlbum2 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 2','test-media-album-2','test description 2');
SET @testAlbum2 = (select Id from MediaAlbums where UrlFriendlyName = 'test-media-album-2')

DECLARE @testAlbum3 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 3','test-media-album-3','test description 3');
SET @testAlbum3 = (select Id from MediaAlbums where UrlFriendlyName = 'test-media-album-3')

DECLARE @testAlbum4 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 4','test-media-album-4','test description 4');
SET @testAlbum4 = (select Id from MediaAlbums where UrlFriendlyName = 'test-media-album-4')

DECLARE @testAlbum5 uniqueidentifier
insert into MediaAlbums (CreatedBy, Created, Active, Name, UrlFriendlyName,Description)
values ('test-runner','2025-12-30T16:25:00',1,'Test Media Album 5','test-media-album-5','test description 5');
SET @testAlbum5 = (select Id from MediaAlbums where UrlFriendlyName = 'test-media-album-5')

insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum1-image1.jpg','A test image1 file','.jpg', @testAlbum1,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum1-image2.jpg','A test image2 file','.jpg', @testAlbum1,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum1-image3.jpg','A test image3 file','.jpg', @testAlbum1,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum1-image4.jpg','A test image4 file','.jpg', @testAlbum1,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum1-image5.jpg','A test image5 file','.jpg', @testAlbum1,0);

insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum2-image1.jpg','A test image1 file','.jpg', @testAlbum2,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum2-image2.jpg','A test image2 file','.jpg', @testAlbum2,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum2-image3.jpg','A test image3 file','.jpg', @testAlbum2,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum2-image4.jpg','A test image4 file','.jpg', @testAlbum2,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum2-image5.jpg','A test image5 file','.jpg', @testAlbum2,0);

insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum3-image1.jpg','A test image1 file','.jpg', @testAlbum3,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum3-image2.jpg','A test image2 file','.jpg', @testAlbum3,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum3-image3.jpg','A test image3 file','.jpg', @testAlbum3,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum3-image4.jpg','A test image4 file','.jpg', @testAlbum3,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum3-image5.jpg','A test image5 file','.jpg', @testAlbum3,0);

insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum4-image1.jpg','A test image1 file','.jpg', @testAlbum4,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum4-image2.jpg','A test image2 file','.jpg', @testAlbum4,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum4-image3.jpg','A test image3 file','.jpg', @testAlbum4,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum4-image4.jpg','A test image4 file','.jpg', @testAlbum4,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum4-image5.jpg','A test image5 file','.jpg', @testAlbum4,0);

insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum5-image1.jpg','A test image1 file','.jpg', @testAlbum5,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum5-image2.jpg','A test image2 file','.jpg', @testAlbum5,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum5-image3.jpg','A test image3 file','.jpg', @testAlbum5,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum5-image4.jpg','A test image4 file','.jpg', @testAlbum5,0);
insert into Media (CreatedBy, Created, Active, FileName, Description, FileExtension, MediaAlbumId, SizeInBytes)
values ('test-runner',getutcdate(),1,
        'mediaalbum5-image5.jpg','A test image5 file','.jpg', @testAlbum5,0);

insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum1, (select id from Tags where Name = 'vacations'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum1, (select id from Tags where Name = 'family'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum1, (select id from Tags where Name = 'hotshots'));

insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum2, (select id from Tags where Name = 'family'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum2, (select id from Tags where Name = 'halloween'));

insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum3, (select id from Tags where Name = 'vacations'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum3, (select id from Tags where Name = 'hotshots'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum3, (select id from Tags where Name = 'family'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum3, (select id from Tags where Name = 'weddings'));

insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum4, (select id from Tags where Name = 'halloween'));

insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum5, (select id from Tags where Name = 'matt'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum5, (select id from Tags where Name = 'heather'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum5, (select id from Tags where Name = 'hunter'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum5, (select id from Tags where Name = 'austin'));
insert into MediaAlbumTags (MediaAlbumId, TagId)
values (@testAlbum5, (select id from Tags where Name = 'family'));

insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum1-image2.jpg'), (select id from Tags where Name = 'halloween'));
insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum1-image2.jpg'), (select id from Tags where Name = 'weddings'));
insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum1-image2.jpg'), (select id from Tags where Name = 'family'));

insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum2-image2.jpg'), (select id from Tags where Name = 'hotshots'));

insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum3-image2.jpg'), (select id from Tags where Name = 'heather'));
insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum3-image2.jpg'), (select id from Tags where Name = 'matt'));

insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum4-image2.jpg'), (select id from Tags where Name = 'vacations'));

insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum5-image2.jpg'), (select id from Tags where Name = 'austin'));
insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum5-image2.jpg'), (select id from Tags where Name = 'matt'));
insert into MediaTags (MediaId, TagId)
values ((select id from Media where FileName = 'mediaalbum5-image2.jpg'), (select id from Tags where Name = 'hunter'));