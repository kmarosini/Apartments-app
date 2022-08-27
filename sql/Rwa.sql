CREATE PROC get_tags
AS
BEGIN
	SELECT * FROM Tag
END

CREATE PROC get_cities
AS
BEGIN
	SELECT * FROM City
END

CREATE PROC getTagUsage
AS 
BEGIN
	SELECT COUNT(TaggedApartment.ApartmentId) AS ukupno, Tag.Name, Tag.Id
	FROM Tag
		LEFT JOIN TaggedApartment on TaggedApartment.TagId = Tag.Id
	GROUP BY Tag.Name, Tag.Id
END
GO

CREATE PROC CreateTag
	@Guid UNIQUEIDENTIFIER,
	@CreatedAt DATETIME2(7),
	@TypeId INT,
	@Name NVARCHAR(50),
	@NameEng NVARCHAR(50)
AS
BEGIN
	INSERT INTO Tag
				(Guid, CreatedAt, TypeId, Name, NameEng)
	VALUES (@Guid, @CreatedAt, @TypeId, @Name, @NameEng)
END

CREATE TABLE LoginDB
(
	UserID INT PRIMARY KEY, 
	username NVARCHAR(50),
	pass NVARCHAR(250)
)

CREATE PROC deleteTag
	@Id INT
AS
BEGIN
	DELETE FROM Tag
	WHERE Tag.Id=@Id
END

CREATE PROC CreateApartment
	@OwnerId INT,
	@TypeId INT,
	@StatusId INT,
	@CityId INT,
	@Adress NVARCHAR(250),
	@Name NVARCHAR(250),
	@NameEng NVARCHAR(250),
	@Price MONEY,
	@MaxAdults INT,
	@MaxChildren INT,
	@TotalRooms INT,
	@BeachDistance INT,
	@CreatedApartment INT OUTPUT
AS
BEGIN
	INSERT INTO Apartment
				(Guid, CreatedAt, DeletedAt, OwnerId, TypeId, StatusId, CityId,Address,Name,NameEng,Price,MaxAdults,MaxChildren,TotalRooms,BeachDistance)
	VALUES (NEWID(), GETDATE(), NULL, @OwnerId, @TypeId, @StatusId, @CityId, @Adress, @Name, @NameEng, @Price, @MaxAdults, @MaxChildren, @TotalRooms, @BeachDistance)
	SET @CreatedApartment = SCOPE_IDENTITY()
END

CREATE PROC getApartments
AS
BEGIN
	SELECT COUNT(ApartmentPicture.ApartmentId) AS Ukupno, ApartmentStatus.Name AS StatusName, Apartment.Id, Apartment.Name AS ApartmentName,City.Name, Apartment.MaxAdults, Apartment.MaxChildren,Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance, ApartmentPicture.Base64Content, ApartmentReview.Stars as ApartmentRating
	FROM Apartment
		LEFT JOIN City ON City.Id = Apartment.CityId
		left join ApartmentStatus ON ApartmentStatus.Id = Apartment.StatusId
		inner join ApartmentPicture ON ApartmentPicture.ApartmentId = Apartment.Id
		left join ApartmentReview ON ApartmentReview.ApartmentId = Apartment.Id
	WHERE Apartment.DeletedAt IS NULL AND ApartmentPicture.IsRepresentative = 1
	GROUP BY Apartment.Name, Apartment.Id, ApartmentStatus.Name, City.Name, Apartment.MaxAdults, Apartment.MaxChildren, Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance, ApartmentPicture.Base64Content, ApartmentPicture.IsRepresentative, ApartmentReview.Stars 
END

CREATE PROC getAllApartmentOwner
BEGIN
	SELECT * FROM ApartmentOwner
end

CREATE PROC getApartmentStatus
AS
BEGIN
	SELECT * FROM ApartmentStatus
END

CREATE PROC updateApartment
	@Id INT,
	@StatusId INT,
	@MaxAdults INT,
	@MaxChildren INT,
	@TotalRooms INT,
	@BeachDistance INT
AS 
BEGIN
	UPDATE Apartment
	SET
		StatusId=@StatusId,
		MaxAdults=@MaxAdults,
		MaxChildren=@MaxChildren,
		TotalRooms=@TotalRooms,
		BeachDistance=@BeachDistance
	WHERE Id=@Id
END

CREATE PROC getPictures
AS
BEGIN
	SELECT COUNT(ApartmentPicture.ApartmentId) AS Ukupno, Apartment.Name, Apartment.Id
	FROM Apartment
		LEFT JOIN ApartmentPicture ON ApartmentPicture.ApartmentId = Apartment.Id
	GROUP BY Apartment.Name, Apartment.Id
END

CREATE PROC getAspUsers
AS
BEGIN
	SELECT AspNetUsers.Id, AspNetUsers.Email, AspNetUsers.PhoneNumber, AspNetUsers.UserName, AspNetUsers.Address
	FROM AspNetUsers
END

CREATE PROC GetApartmentUsers
AS 
BEGIN
	SELECT ApartmentReservation.Id, ApartmentReservation.ApartmentId, ApartmentReservation.UserId, Apartment.Name, AspNetUsers.UserName
	FROM ApartmentReservation
		LEFT JOIN Apartment ON Apartment.Id = ApartmentReservation.Id
		LEFT JOIN AspNetUsers ON AspNetUsers.Id = ApartmentReservation.Id
	GROUP BY Apartment.Name, ApartmentReservation.Id, ApartmentReservation.ApartmentId, ApartmentReservation.UserId, AspNetUsers.UserName
END

CREATE PROC getApartmentById
	@Id INT
AS 
BEGIN
	SELECT COUNT(ApartmentPicture.ApartmentId) AS Ukupno, ApartmentStatus.Name AS StatusName, Apartment.Id, Apartment.Name AS ApartmentName,City.Name, Apartment.MaxAdults, Apartment.MaxChildren,Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance, ApartmentPicture.Base64Content
	FROM Apartment
		LEFT JOIN City ON City.Id = Apartment.CityId
		LEFT JOIN ApartmentStatus ON ApartmentStatus.Id = Apartment.StatusId
		LEFT JOIN ApartmentPicture ON ApartmentPicture.ApartmentId = Apartment.Id
	WHERE Apartment.DeletedAt IS NULL AND ApartmentPicture.IsRepresentative = 1 AND Apartment.Id = @Id
	GROUP BY Apartment.Name, Apartment.Id, ApartmentStatus.Name, City.Name, Apartment.MaxAdults, Apartment.MaxChildren, Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance, ApartmentPicture.Base64Content, ApartmentPicture.IsRepresentative
END

CREATE PROC SoftDelete
	@Id INT
AS
BEGIN
	UPDATE Apartment 
		SET DeletedAt = GETDATE()
	WHERE Id = @Id
END

CREATE PROC GetUsedTags
	@Id INT
AS
BEGIN
	SELECT Tag.Name
	FROM TaggedApartment
		left join Tag ON Tag.Id = TaggedApartment.TagId
	WHERE ApartmentId = @Id
END


CREATE PROC GetUnusedTags
	@Id INT
AS
BEGIN
	SELECT DISTINCT Tag.Name
	FROM TaggedApartment
	RIGHT JOIN Tag ON Tag.Id = TaggedApartment.TagId
	WHERE Tag.Id NOT IN (
	SELECT DISTINCT Tag.Id
	FROM Tag
	LEFT JOIN TaggedApartment ON TaggedApartment.TagId = Tag.Id
	WHERE TaggedApartment.ApartmentId = @Id
	)
end

CREATE PROC InsertIntoTags
	@Id INT,
	@TagId INT
AS 
BEGIN
	INSERT INTO TaggedApartment (Guid,ApartmentId,TagId)
	VALUES (NEWID(), @Id, @TagId)
END

CREATE PROC InsertIntoUsers
	@Email NVARCHAR(256),
	@PhoneNumber NVARCHAR,
	@UserName NVARCHAR(256),
	@Address NVARCHAR(1000)
AS 
BEGIN
	INSERT INTO AspNetUsers
			(Guid, CreatedAt, DeletedAt, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, Address)
	VALUES(NEWID(), GETDATE(), NULL, @Email, 1, NULL, NULL, @PhoneNumber, 1, NULL, 0, 0, @UserName, @Address)
END

CREATE PROC InsertNotRegisteredResevation
	@ApartmentId INT,
	@Details NVARCHAR(1000),
	@UserId NVARCHAR(256),
	@UserName NVARCHAR(250),
	@UserEmail NVARCHAR(250),
	@UserPhone NVARCHAR(20), 
	@UserAdress NVARCHAR(1000)
AS 
BEGIN
	INSERT INTO ApartmentReservation
			(Guid, CreatedAt, ApartmentId, Details, UserId, UserName, UserEmail, UserPhone, UserAddress) 
	VALUES(NEWID(), GETDATE(), @ApartmentId, @Details, @UserId, @UserName, @UserEmail, @UserPhone, @UserAdress)
END

CREATE PROC SavePicture
	@ApartmentId INT,
	@Name NVARCHAR(250),
	@Base64Content VARBINARY(MAX) 
AS
BEGIN
	IF EXISTS (SELECT * FROM ApartmentPicture WHERE ApartmentId=@ApartmentId AND IsRepresentative = 1)
	INSERT INTO ApartmentPicture (Guid, CreatedAt, DeletedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
	VALUES (NEWID(), GETDATE(), NULL, @ApartmentId, NULL, @Base64Content, @Name, 0);

	ELSE
	INSERT INTO ApartmentPicture (Guid, CreatedAt, DeletedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
	VALUES (NEWID(), GETDATE(), NULL, @ApartmentId, NULL, @Base64Content, @Name, 1);
END

CREATE PROC GetApartmentPictures 
  @ApartmentId INT
AS 
BEGIN
	SELECT ApartmentPicture.ApartmentId, ApartmentPicture.Name, ApartmentPicture.Path, ApartmentPicture.IsRepresentative, ApartmentPicture.Id, ApartmentPicture.Base64Content
	FROM ApartmentPicture
	WHERE ApartmentId = @ApartmentId
END


CREATE PROC SetAsRepresentative
	@ApartmentId INT,
	@Id INT --pictureId
AS
BEGIN
	UPDATE ApartmentPicture SET IsRepresentative = 0
	WHERE ApartmentId=@ApartmentId
	UPDATE ApartmentPicture SET IsRepresentative = 1
	WHERE ApartmentPicture.Id=@Id
END

CREATE PROC SaveApartmentReview
	@ApartmentId INT,
	@UserId INT,
	@Details NVARCHAR(1000),
	@Stars INT
AS
BEGIN
	INSERT INTO ApartmentReview(Guid, CreatedAt, ApartmentId, UserId, Details, Stars)
	VALUES ( NEWID(), GETDATE(), @ApartmentId, @UserId, @Details, @Stars);
END

CREATE PROC SaveReservationForUnregisteredUsers
	@ApartmentId INT,
	@Details NVARCHAR(1000),
	@UserName NVARCHAR(250),
	@UserEmail NVARCHAR(250),
	@UserPhone NVARCHAR(250),
	@UserAddress NVARCHAR(250)
AS
BEGIN
	INSERT INTO ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserName, UserEmail, UserPhone, UserAddress)
	VALUES ( NEWID(), GETDATE(), @ApartmentId, @Details, @UserName,@UserEmail, @UserPhone, @UserAddress);
END

CREATE PROC SaveReservationForRegisteredUsers
	@ApartmentId INT,
	@Details NVARCHAR(1000),
	@UserId INT
AS
BEGIN
	INSERT INTO ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserId)
	VALUES ( NEWID(), GETDATE(), @ApartmentId, @Details, @UserId)
END

CREATE PROC AuthUser
	@Email NVARCHAR(50),
	@password NVARCHAR(128)
AS
BEGIN
	SELECT * 
	FROM AspNetUsers
	WHERE Email=@Email AND PasswordHash=@password 
END
GO

CREATE PROC RegisterUser
	@Email NVARCHAR(50),
	@PasswordHash nvarchar(MAX),
	@PhoneNumber NVARCHAR(MAX),
	@UserName NVARCHAR(50),
	@Address NVARCHAR(128)
AS
BEGIN
	INSERT INTO AspNetUsers
	VALUES (NEWID(), GETDATE(), NULL, @Email, 1, @PasswordHash, NULL, @PhoneNumber, 1, NULL, 0, 0, @UserName, @Address )
END
GO

ALTER TABLE ApartmentPicture
DROP COLUMN Base64content

ALTER TABLE ApartmentPicture
ADD Base64Content VARBINARY(MAX)


INSERT INTO LoginDB(UserID ,username, pass)
VALUES (1,'admin', '123')

