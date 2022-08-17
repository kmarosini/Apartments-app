using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;

namespace DataLayer.Dal
{
    public class DBRepo : IRepo
    {
        private static string CS = @"Data Source=DESKTOP-958MSQ8\SQLEXPRESS2;Initial Catalog=RwaApartmani;Integrated Security=True";
        private static string APARTMENTS_CS = @"Data Source=DESKTOP-958MSQ8\SQLEXPRESS2;Initial Catalog=RwaApartmani;Integrated Security=True";

        private SqlConnection connection;
        private SqlCommand command;



        public IList<ApartmentTags> LoadApartmentTags()
        {
            IList<ApartmentTags> tags = new List<ApartmentTags>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getTagUsage").Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new ApartmentTags
                    {
                        ukupno = (int)row[nameof(ApartmentTags.ukupno)],
                        name = row[nameof(ApartmentTags.name)].ToString(),
                        Id = (int)row[nameof(ApartmentTags.Id)]
                    }
                );

            }

            return tags;
        }

        public void DeleteTag(int id)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, "deleteTag", id);
        }

        public void GetApartmentById(Apartment apartment, int id)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, "GetApartmentById", apartment.StatusName, apartment.ApartmentName, apartment.MaxChildren, apartment.MaxAdults, apartment.TotalRooms, apartment.Price, apartment.CityId, apartment.StatusId, apartment.BeachDistance , apartment.Name , id);
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

     

        public IList<City> LoadCities()
        {
            IList<City> cities = new List<City>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, "get_cities").Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                cities.Add(
                    new City
                    {
                        Id = (int)row[nameof(City.Id)],
                        Name = row[nameof(City.Name)].ToString(),
                    }
                );
            }

            return cities;
        }

        public IList<User> LoadUsers()
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();
            var aps = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getApartments").Tables[0];
            foreach (DataRow row in aps.Rows)
            {
                apartments.Add(
                   new Apartment
                   {
                       Name = row[nameof(Apartment.Name)].ToString(),
                       ApartmentName = row[nameof(Apartment.ApartmentName)].ToString(),
                       Id = (int)row[nameof(Apartment.Id)],
                       MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                       MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                       TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                       BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                       Price = Math.Round((decimal)row[nameof(Apartment.Price)]),
                       CityId = (int)row[nameof(Apartment.CityId)],
                       StatusId = (int)row[nameof(Apartment.StatusId)],
                       StatusName = (string)row[nameof(Apartment.StatusName)],
                       Base64Content = (byte[])(row[nameof(Apartment.Base64Content)] == System.DBNull.Value ? null : row[nameof(Apartment.Base64Content)]),
                       Ukupno = (int)row[nameof(Apartment.Ukupno)],
                       ApartmentRating = (int)(row[nameof(Apartment.ApartmentRating)] == DBNull.Value ? 0 : row[nameof(Apartment.ApartmentRating)]),
                       ImageString = (row[nameof(Apartment.Base64Content)] == DBNull.Value ? null : Convert.ToBase64String((byte[])row[nameof(Apartment.Base64Content)]))
                   }
               ) ;
            }

            return apartments;
        }

        public void CreateTag(Tags tag)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(CreateTag), tag.Guid, tag.CreatedAt, tag.TypeId, tag.Name, tag.NameEng);
        }

        public int CreateApartment(Apartment apartment)
        {
            //SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(CreateApartment), apartment.OwnerId, apartment.TypeId, apartment.StatusId, apartment.CityId, apartment.Adress, apartment.Name, apartment.NameEng, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance);

            using (connection = new SqlConnection(@"Data Source=DESKTOP-958MSQ8\SQLEXPRESS2;Initial Catalog=RwaApartmani;Integrated Security=True"))
            {
                connection.Open();
                command = new SqlCommand("CreateApartment", connection);
                command.Parameters.AddWithValue("OwnerId", apartment.OwnerId);
                command.Parameters.AddWithValue("TypeId", apartment.TypeId);
                command.Parameters.AddWithValue("StatusId", apartment.StatusId);
                command.Parameters.AddWithValue("CityId", apartment.CityId);
                command.Parameters.AddWithValue("Adress", apartment.Adress);
                command.Parameters.AddWithValue("Name", apartment.Name);
                command.Parameters.AddWithValue("NameEng", apartment.NameEng);
                command.Parameters.AddWithValue("Price", apartment.Price);
                command.Parameters.AddWithValue("MaxAdults", apartment.MaxAdults);
                command.Parameters.AddWithValue("MaxChildren", apartment.MaxChildren);
                command.Parameters.AddWithValue("TotalRooms", apartment.TotalRooms);
                command.Parameters.AddWithValue("BeachDistance", apartment.BeachDistance);
                command.Parameters.Add("CreatedApartment", System.Data.SqlDbType.Int);
                command.Parameters["CreatedApartment"].Direction = System.Data.ParameterDirection.Output;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                return Convert.ToInt32(command.Parameters["CreatedApartment"].Value);
            }
        }

        public IList<ApartmentOwner> LoadApartmentOwner()
        {
            IList<ApartmentOwner> apartmentOwners = new List<ApartmentOwner>();

            var tblApartmentOwners = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getAllApartmentOwner").Tables[0];
            foreach (DataRow row in tblApartmentOwners.Rows)
            {
                apartmentOwners.Add(
                    new ApartmentOwner
                    {
                        Id = (int)row[nameof(ApartmentOwner.Id)],
                        Guid = (Guid)row[nameof(ApartmentOwner.Guid)],
                        CreatedAt = (DateTime)row[nameof(ApartmentOwner.CreatedAt)],
                        Name = (string)row[nameof(ApartmentOwner.Name)]
                    }
                );

            }

            return apartmentOwners;
        }

        public IList<ApartmentStatus> LoadApartmentStatus()
        {
            IList<ApartmentStatus> apartmentStatus = new List<ApartmentStatus>();

            var tblApartmentStatus = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getApartmentStatus").Tables[0];
            foreach (DataRow row in tblApartmentStatus.Rows)
            {
                apartmentStatus.Add(
                    new ApartmentStatus
                    {
                        Id= (int)row[nameof(ApartmentStatus.Id)],
                        Guid= (Guid)row[nameof(ApartmentStatus.Guid)],
                        Name = (string)row[nameof(ApartmentStatus.Name)],
                        NameEng = (string)row[nameof(ApartmentStatus.NameEng)]
                    }
                );

            }

            return apartmentStatus;
        }

        public void UpdateApartment(Apartment apartment)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(UpdateApartment),apartment.Id,apartment.StatusId,apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance);
        }

        public IList<ApartmentPicture> GetApartmentPictures(int id)
        {
            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();

            var tblApartmentPicture = SqlHelper.ExecuteDataset(APARTMENTS_CS, "GetApartmentPictures", id).Tables[0];
            foreach (DataRow row in tblApartmentPicture.Rows)
            {
                apartmentPictures.Add(
                    new ApartmentPicture
                    {
                        Name = (string)row[nameof(ApartmentPicture.Name)],
                        ApartmentId = (int)row[nameof(ApartmentPicture.ApartmentId)],
                        Id = (int)row[nameof(ApartmentPicture.Id)],
                        Base64Content = row[nameof(ApartmentPicture.Base64Content)] == System.DBNull.Value ? null : (byte[])row[nameof(ApartmentPicture.Base64Content)]
                    }
                );

            }

            return apartmentPictures;
        }

        public IList<AspNetUsers> LoadAspNetUsers()
        {
            IList<AspNetUsers> users = new List<AspNetUsers>();

            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getAspUsers").Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new AspNetUsers
                    {
                        Id = (int)row[nameof(AspNetUsers.Id)],
                        Email = (string)row[nameof(AspNetUsers.Email)],
                        PhoneNumber = (string)row[nameof(AspNetUsers.PhoneNumber)],
                        UserName = (string)row[nameof(AspNetUsers.UserName)],
                        Address = (string)row[nameof(AspNetUsers.Address)]        
                    }
                );

            }

            return users;
        }

        public Apartment getApartmentById(int id)
        {
            var apart = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getApartmentById", id).Tables[0];
            Apartment test = null;
            foreach (DataRow row in apart.Rows)
            {
                test = new Apartment
                {
                    Name = row[nameof(Apartment.Name)].ToString(),
                    ApartmentName = row[nameof(Apartment.ApartmentName)].ToString(),
                    Id = (int)row[nameof(Apartment.Id)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Price = Math.Round((decimal)row[nameof(Apartment.Price)]),
                    CityId = (int)row[nameof(Apartment.CityId)],
                    StatusId = (int)row[nameof(Apartment.StatusId)],
                    StatusName = (string)row[nameof(Apartment.StatusName)],
                    Base64Content = row[nameof(Apartment.Base64Content)] == System.DBNull.Value ? null : (byte[])row[nameof(Apartment.Base64Content)],
                    Ukupno = (int)row[nameof(Apartment.Ukupno)]
                };
            }

            return test;
        }

        public void SoftDelete(int id)
        {
            var delete = SqlHelper.ExecuteNonQuery(APARTMENTS_CS, "SoftDelete", id);
        }

        public IList<Tags> GetUsedTags(int id)
        {
            var tagovi = SqlHelper.ExecuteDataset(APARTMENTS_CS, "GetUsedTags", id).Tables[0];
            IList<Tags> tags = new List<Tags>();  
            foreach (DataRow row in tagovi.Rows)
            {
                tags.Add(new Tags
                {
                    Name = (string)row[nameof(Tags.Name)]
                });
            }

            return tags;
        }

        public IList<Tags> GetUnusedTags(int id)
        {
            var apart = SqlHelper.ExecuteDataset(APARTMENTS_CS, "GetUnusedTags", id).Tables[0];
            IList<Tags> tagova = new List<Tags>();
            foreach (DataRow row in apart.Rows)
            {
                tagova.Add(new Tags
                {
                    Name = (string)row[nameof(Tags.Name)]
                });
            }

            return tagova;
        }

        public void InsertIntoTags(int id, int TagId)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, "InsertIntoTags", id, TagId);
        }

        public void InsertNotRegisteredResevation(ApartmentReservation reservation)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(InsertNotRegisteredResevation),reservation.ApartmentId, reservation.Details, reservation.UserId, reservation.UserName, reservation.UserEmail, reservation.UserPhone, reservation.UserAdress);

        }
        
        public IList<AspNetUsers> LoadRegisteredUsers()
        {
            IList<AspNetUsers> users = new List<AspNetUsers>();

            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, "getAspUsers").Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new AspNetUsers
                    {
                        UserName = (string)row[nameof(AspNetUsers.UserName)]
                    }
                );

            }

            return users;
        }

        public void SavePicture(ApartmentPicture picture)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SavePicture),  picture.ApartmentId, picture.Name, picture.Base64Content);
        }

        public void SetAsRepresentative(int apatmentId, int id)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, "SetAsRepresentative", apatmentId,  id);

        }

        public void SaveApartmentReview(ApartmentReview review)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SaveApartmentReview), review.ApartmentId, review.UserId, review.Details, review.Stars);
        }

        public void SaveReservationForUnregisteredUsers(ApartmentReservation reservation)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SaveReservationForUnregisteredUsers), reservation.ApartmentId, reservation.Details, reservation.UserName, reservation.UserEmail, reservation.UserPhone, reservation.UserAdress);
        }

        public void RegisterUser(AspNetUsers user)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(RegisterUser), user.Email, user.PasswordHash, user.PhoneNumber, user.UserName, user.Address);
        }

        public AspNetUsers AuthUser(string email, string password)
        {
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, "AuthUser", email, Cryptography.HashPassword(password)).Tables[0];
            List<AspNetUsers> users = new List<AspNetUsers>();

            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new AspNetUsers
                    {
                        UserName = (string)row[nameof(AspNetUsers.UserName)],
                        PhoneNumber = (string)row[nameof(AspNetUsers.PhoneNumber)],
                        Address = (string)row[nameof(AspNetUsers.Address)],
                        Email = (string)row[nameof(AspNetUsers.Email)],
                        Id = (int)row[nameof(AspNetUsers.Id)]
                    }
                );

            }

            if (users.Count == 0)
            {
                return null;
            }
            return users[0];
        }

    }
}
