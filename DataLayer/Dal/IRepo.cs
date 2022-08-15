using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dal
{
    public interface IRepo
    {
        IList<City> LoadCities();
        IList<User> LoadUsers();
        IList<ApartmentStatus> LoadApartmentStatus();
        IList<ApartmentOwner> LoadApartmentOwner();
        IList<Apartment> LoadApartments();
        IList<ApartmentTags> LoadApartmentTags();
        IList<ApartmentPicture> GetApartmentPictures(int id);
        IList<AspNetUsers> LoadAspNetUsers();
        IList<AspNetUsers> LoadRegisteredUsers();
        void SaveUser(User user);
        void AddUser(User user);
        void DeleteTag(int id);
        void CreateTag(Tags tag);
        void SavePicture(ApartmentPicture picture);
        void InsertIntoTags(int id, int TagId);

        void InsertNotRegisteredResevation(ApartmentReservation reservation);
        Apartment getApartmentById(int id);
        IList<Tags> GetUsedTags(int id);
        IList<Tags> GetUnusedTags(int id);
        void SoftDelete(int id);
        void SetAsRepresentative(int apatmentId, int id);
        int CreateApartment(Apartment apartment);
        void UpdateApartment(Apartment apartment);
        void SaveApartmentReview(ApartmentReview review);
        void SaveReservationForUnregisteredUsers(ApartmentReservation reservation);

        void RegisterUser(AspNetUsers user);

        AspNetUsers AuthUser(string email, string password);
    }
}
