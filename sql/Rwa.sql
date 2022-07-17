use RwaApartmani

Create proc get_tags
as
begin
select * from Tag
end

Create proc get_cities
as
begin
select * from City
end

create proc getTagUsage
as 
begin
select COUNT(TaggedApartment.ApartmentId) as ukupno, Tag.Name, Tag.Id
from Tag
LEFT JOIN TaggedApartment on TaggedApartment.TagId = Tag.Id
group by Tag.Name, Tag.Id
end
go

create proc CreateTag
	@Guid uniqueidentifier,
	@CreatedAt datetime2(7),
	@TypeId int,
	@Name nvarchar(50),
	@NameEng nvarchar(50)
as
begin
	insert into Tag
				(Guid, CreatedAt, TypeId, Name, NameEng)
	values (@Guid, @CreatedAt, @TypeId, @Name, @NameEng)
end

create table LoginDB
(
	UserID int primary key, 
	username nvarchar(50),
	pass nvarchar(250)
)

create proc deleteTag
	@Id int
as
begin
delete from Tag
where Tag.Id=@Id
end

alter proc CreateApartment
	@OwnerId int,
	@TypeId int,
	@StatusId int,
	@CityId int,
	@Adress nvarchar(250),
	@Name nvarchar(250),
	@NameEng nvarchar(250),
	@Price money,
	@MaxAdults int,
	@MaxChildren int,
	@TotalRooms int,
	@BeachDistance int
as
begin
	insert into Apartment
				(Guid, CreatedAt, DeletedAt, OwnerId, TypeId, StatusId, CityId,Address,Name,NameEng,Price,MaxAdults,MaxChildren,TotalRooms,BeachDistance)
	values (NEWID(), GETDATE(), NULL, @OwnerId, @TypeId, @StatusId, @CityId, @Adress, @Name, @NameEng, @Price, @MaxAdults, @MaxChildren, @TotalRooms, @BeachDistance)
end

alter proc getApartments
as
begin
select COUNT(ApartmentPicture.ApartmentId) as Ukupno, ApartmentStatus.Name as StatusName, Apartment.Id, Apartment.Name as ApartmentName,City.Name, Apartment.MaxAdults, Apartment.MaxChildren,Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance, ApartmentPicture.Base64Content
from Apartment
LEFT JOIN City on City.Id = Apartment.CityId
left join ApartmentStatus on ApartmentStatus.Id = Apartment.StatusId
left join ApartmentPicture on ApartmentPicture.ApartmentId = Apartment.Id
where Apartment.DeletedAt is NULL AND ApartmentPicture.IsRepresentative = 1
group by Apartment.Name, Apartment.Id, ApartmentStatus.Name, City.Name, Apartment.MaxAdults, Apartment.MaxChildren, Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance, ApartmentPicture.Base64Content, ApartmentPicture.IsRepresentative
end

create proc getAllApartmentOwner
as
begin
select * from ApartmentOwner
end

create proc getApartmentStatus
as
begin
select * from ApartmentStatus
end

alter proc updateApartment
	@Id int,
	@StatusId int,
	@MaxAdults int,
	@MaxChildren int,
	@TotalRooms int,
	@BeachDistance int
as 
begin
update Apartment
set
StatusId=@StatusId,
MaxAdults=@MaxAdults,
MaxChildren=@MaxChildren,
TotalRooms=@TotalRooms,
BeachDistance=@BeachDistance
where Id=@Id
end

create proc getPictures
as
begin
select COUNT(ApartmentPicture.ApartmentId) as Ukupno, Apartment.Name, Apartment.Id
from Apartment
left join ApartmentPicture on ApartmentPicture.ApartmentId = Apartment.Id
group by Apartment.Name, Apartment.Id
end

create proc getAspUsers
as
begin
select AspNetUsers.Id, AspNetUsers.Email, AspNetUsers.PhoneNumber, AspNetUsers.UserName, AspNetUsers.Address
from AspNetUsers
end

create proc GetApartmentUsers
as 
begin
select ApartmentReservation.Id, ApartmentReservation.ApartmentId, ApartmentReservation.UserId, Apartment.Name, AspNetUsers.UserName
from ApartmentReservation
left join Apartment on Apartment.Id = ApartmentReservation.Id
left join AspNetUsers on AspNetUsers.Id = ApartmentReservation.Id
group by Apartment.Name, ApartmentReservation.Id, ApartmentReservation.ApartmentId, ApartmentReservation.UserId, AspNetUsers.UserName
end

create proc getApartmentById
	@Id int
as 
begin
select COUNT(ApartmentPicture.ApartmentId) as Ukupno, ApartmentStatus.Name as StatusName,Apartment.Id, Apartment.Name as ApartmentName,City.Name, Apartment.MaxAdults, Apartment.MaxChildren,Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance
from Apartment
LEFT JOIN City on City.Id = Apartment.CityId
left join ApartmentStatus on ApartmentStatus.Id = Apartment.StatusId
left join ApartmentPicture on ApartmentPicture.ApartmentId = Apartment.Id
where Apartment.Id = @Id
group by Apartment.Name, Apartment.Id, ApartmentStatus.Name, City.Name, Apartment.MaxAdults, Apartment.MaxChildren, Apartment.TotalRooms, Apartment.Price, Apartment.CityId, Apartment.StatusId, Apartment.BeachDistance
end

create proc SoftDelete
	@Id int
as
begin
update Apartment 
set DeletedAt = GETDATE()
where Id = @Id
end

exec SoftDelete 14
exec getApartmentById 1

alter proc GetUsedTags
	@Id int
as
begin
select Tag.Name
from TaggedApartment
left join Tag on Tag.Id = TaggedApartment.TagId
where ApartmentId = @Id
end


alter proc GetUnusedTags
	@Id int
as
begin
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

create proc InsertIntoTags
	@Id int,
	@TagId int
as 
begin
	insert into TaggedApartment (Guid,ApartmentId,TagId)
	values (NEWID(), @Id, @TagId)
end

create proc InsertIntoUsers
	@Email nvarchar(256),
	@PhoneNumber nvarchar,
	@UserName nvarchar(256),
	@Address nvarchar(1000)
as 
begin
	insert into AspNetUsers
			(Guid, CreatedAt, DeletedAt, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, Address)
	values(NEWID(), GETDATE(), NULL, @Email, 1, NULL, NULL, @PhoneNumber, 1, NULL, 0, 0, @UserName, @Address)
end

alter proc InsertNotRegisteredResevation
	@ApartmentId int,
	@Details nvarchar(1000),
	@UserId nvarchar(256),
	@UserName nvarchar(250),
	@UserEmail nvarchar(250),
	@UserPhone nchar(20),
	@UserAdress nvarchar(1000)
as 
begin
	insert into ApartmentReservation
			(Guid, CreatedAt, ApartmentId, Details, UserId, UserName, UserEmail, UserPhone, UserAddress) 
	values(NEWID(), GETDATE(), @ApartmentId, @Details, @UserId, @UserName, @UserEmail, @UserPhone, @UserAdress)
end

alter proc SavePicture
	@ApartmentId int,
	@Name nvarchar(250),
	@Base64Content varbinary(max) 
as
begin
if exists (Select * from ApartmentPicture where ApartmentId=@ApartmentId and IsRepresentative = 1)
insert into ApartmentPicture (Guid, CreatedAt, DeletedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
values (NEWID(), GETDATE(), NULL, @ApartmentId, null, @Base64Content, @Name, 0);

else
insert into ApartmentPicture (Guid, CreatedAt, DeletedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
values (NEWID(), GETDATE(), NULL, @ApartmentId, null, @Base64Content, @Name, 1);
end

alter proc GetApartmentPictures 
  @ApartmentId int
as 
begin
select ApartmentPicture.ApartmentId, ApartmentPicture.Name, ApartmentPicture.Path, ApartmentPicture.IsRepresentative, ApartmentPicture.Id, ApartmentPicture.Base64Content
from ApartmentPicture
where ApartmentId = @ApartmentId
end


alter proc SetAsRepresentative
	@ApartmentId int,
	@Id int
as
begin
update ApartmentPicture set IsRepresentative = 1
where ApartmentId=@ApartmentId
update ApartmentPicture set IsRepresentative = 0
where Id!=@Id
end


create proc SaveApartmentReview
	@ApartmentId int,
	@UserId int,
	@Details nvarchar(1000),
	@Stars int
as
begin
insert into ApartmentReview(Guid, CreatedAt, ApartmentId, UserId, Details, Stars)
values ( NEWID(), GETDATE(), @ApartmentId, @UserId, @Details, @Stars);
end

create proc SaveReservationForUnregisteredUsers
	@ApartmentId int,
	@Details nvarchar(1000),
	@UserName nvarchar(250),
	@UserEmail nvarchar(250),
	@UserPhone nvarchar(250),
	@UserAddress nvarchar(250)
as
begin
insert into ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserName, UserEmail, UserPhone, UserAddress)
values ( NEWID(), GETDATE(), @ApartmentId, @Details, @UserName,@UserEmail, @UserPhone, @UserAddress);
end

create proc SaveReservationForRegisteredUsers
	@ApartmentId int,
	@Details nvarchar(1000),
	@UserId int
as
begin
insert into ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserId)
values ( NEWID(), GETDATE(), @ApartmentId, @Details, @UserId)
end

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

alter PROC RegisterUser
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

exec GetApartmentPictures 3

delete from ApartmentPicture

alter table ApartmentPicture
add Base64Content VARBINARY(MAX)



select * from Tag
select * from LoginDB where username='admin'
select * from TaggedApartment
select * from ApartmentPicture
select * from Apartment
select * from LoginDB
select * from City
select * from ApartmentOwner
select * from ApartmentStatus
select * from AspNetUsers
select * from AspNetRoles
select * from AspNetUserLogins
select * from ApartmentReview
select * from ApartmentReservation
select * from TagType
select * from TaggedApartment

--apartmane i njihove base64, where representative, u selectu dodat novi column base64