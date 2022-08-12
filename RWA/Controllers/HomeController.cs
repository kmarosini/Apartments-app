using RWA.Models;
using DataLayer.Dal;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ApartmentsMVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ShowAllApartments()
        {
            IList<DataLayer.Model.Apartment> _listOfAllApartments = RepoFactory.GetRepo().LoadApartments();
            var cities = RepoFactory.GetRepo().LoadCities();
            ViewBag.Cities = cities;

            return View(new ApartmentVM
            {
                ListaApartmana = _listOfAllApartments.ToList()
            });
        }


        [HttpGet]
        public ActionResult ApartmentDetails(int id)
        {
            var apartment = RepoFactory.GetRepo().getApartmentById(id);
            var tags = RepoFactory.GetRepo().GetUsedTags(id);

            ViewBag.Apartment = apartment;
            ViewBag.Tags = tags;

            return View("ApartmentDetails");
        }

        [HttpPost]
        public ActionResult ApartmentDetails(RWA.Models.ApartmentReservation reservation)
        {
            if (!ModelState.IsValid)
            {
                GetApartmentDetails();

                return View("ApartmentDetails", new ReviewReservationViewModel { Reservation = reservation });
            }


            RepoFactory.GetRepo().SaveReservationForUnregisteredUsers(new DataLayer.Model.ApartmentReservation
            {
                UserAdress = reservation.UserAddress,
                UserName = reservation.UserName,
                UserEmail = reservation.UserEmail,
                UserPhone = reservation.UserPhone,
                Details = reservation.Details,
                ApartmentId = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString())
            });

            return View("ShowAllApartments");
        }

        private void GetApartmentDetails()
        {
            var apartment = RepoFactory.GetRepo().getApartmentById(int.Parse(Url.RequestContext.RouteData.Values["id"].ToString()));
            var tags = RepoFactory.GetRepo().GetUsedTags(int.Parse(Url.RequestContext.RouteData.Values["id"].ToString()));

            ViewBag.Apartment = apartment;
            ViewBag.Tags = tags;
        }

        [HttpPost]
        public ActionResult CreateApartmentReview(RWA.Models.ApartmentReview a)
        {
                //var apartment = RepoFactory.GetRepo().getApartmentById(int.Parse(Url.RequestContext.RouteData.Values["id"].ToString()));

            if (!ModelState.IsValid)
            {
 
                return RedirectToAction("ApartmentDetails", new ReviewReservationViewModel { Reservation = new RWA.Models.ApartmentReservation(), Review = a });
            }

            RepoFactory.GetRepo().SaveApartmentReview(new DataLayer.Model.ApartmentReview
            {
                ApartmentId = 2,
                Details = a.Details,
                UserId = 1,
                Stars = a.Stars
            });

            return RedirectToAction("ShowAllApartments");
        }
        [HttpPost]
        public ActionResult FilterApartments(RWA.Models.Filter filter)
        {
            IList<DataLayer.Model.Apartment> _listOfAllApartments = RepoFactory.GetRepo().LoadApartments();
            var cities = RepoFactory.GetRepo().LoadCities();
            ViewBag.Cities = cities;
             
            if (!ModelState.IsValid)
            {
                return View("ShowAllApartments", new ApartmentVM { ListaApartmana = (List<Apartment>)_listOfAllApartments });
            }

            var PmtFreq = Request.Form["gradField"];


            List<Apartment> list = _listOfAllApartments.ToList().FindAll(a => a.CityId == filter.GradId);

            return View("ShowAllApartments", new ApartmentVM { ListaApartmana = list });
        }
    }
}