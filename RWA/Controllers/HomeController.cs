using RWA.Models;
using DataLayer.Dal;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

namespace ApartmentsMVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ShowAllApartments()
        {
            IList<DataLayer.Model.Apartment> _listOfAllApartments = RepoFactory.GetRepo().LoadApartments();
            var cities = RepoFactory.GetRepo().LoadCities();

            

            string[] vs = HttpContext.Request.Cookies["sortingFilterOptions"]?.Value.ToString().Split('|');


            if (vs == null)
            {
                ViewBag.Cities = cities;
                return View(new ApartmentVM
                {
                    ListaApartmana = _listOfAllApartments.ToList(),
                    Filter = null
                });
            }
            else
            {
                RWA.Models.Filter filter = new RWA.Models.Filter
                {
                    GradId = int.Parse(vs[0]),
                    RoomNumber = int.Parse(vs[1]),
                    Adults = int.Parse(vs[2]),
                    Children = int.Parse(vs[3])
                };
                ViewBag.Cities = cities;

                return View(new ApartmentVM
                {
                    ListaApartmana = _listOfAllApartments.ToList(),
                    Filter = filter
                });
            }
        }


        [HttpGet]
        public ActionResult ApartmentDetails(int id)
        {
            var apartment = RepoFactory.GetRepo().getApartmentById(id);
            var tags = RepoFactory.GetRepo().GetUsedTags(id);
            var pictures = RepoFactory.GetRepo().GetApartmentPictures(id);

            Session["currentApartment"] = id;

            ViewBag.Apartment = apartment;
            ViewBag.Tags = tags;
            ViewBag.Pictures = pictures;

            return View("ApartmentDetails");
        }

        [HttpPost]
        public ActionResult ApartmentDetails(RWA.Models.ApartmentReservation reservation)
        {
            if (!ModelState.IsValid)
            {
                
                GetApartmentDetails();
                var pictures = RepoFactory.GetRepo().GetApartmentPictures((int)Session["currentApartment"]);
                ViewBag.Pictures = pictures;


                return View("ApartmentDetails", new ReviewReservationViewModel { Reservation = reservation });
            }

            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Captcha answer cannot be empty.");
                GetApartmentDetails();
                var pictures = RepoFactory.GetRepo().GetApartmentPictures((int)Session["currentApartment"]);
                ViewBag.Pictures = pictures;

                return View("ApartmentDetails", new ReviewReservationViewModel { Reservation = reservation });
            }
            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (!recaptchaResult.Success)
            {
                GetApartmentDetails();
                var pictures = RepoFactory.GetRepo().GetApartmentPictures((int)Session["currentApartment"]);
                ViewBag.Pictures = pictures;

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
            var cities = RepoFactory.GetRepo().LoadCities();
            ViewBag.Cities = cities;
            return View("ShowAllApartments", new ApartmentVM { Filter = null, ListaApartmana = (List<Apartment>)RepoFactory.GetRepo().LoadApartments() });
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

            int id = (int)Session["currentApartment"];

            int stars = a.Stars;
            string details = a.Details;


            RepoFactory.GetRepo().SaveApartmentReview(new DataLayer.Model.ApartmentReview
            {

                ApartmentId = id,
                Details = details,
                UserId = ((AspNetUsers)Session["user"]).Id,
                Stars = stars
            });

            return RedirectToAction("ShowAllApartments");
        }
        [HttpPost]
        public ActionResult FilterApartments(RWA.Models.Filter filter)
        {
            IList<DataLayer.Model.Apartment> _listOfAllApartments = RepoFactory.GetRepo().LoadApartments();
            var cities = RepoFactory.GetRepo().LoadCities();

            HttpContext.Response.Cookies.Add(new HttpCookie("sortingFilterOptions", $"{filter.GradId}|{filter.RoomNumber}|{filter.Adults}|{filter.Children}"));

            ViewBag.Cities = cities;
             
            if (!ModelState.IsValid)
            {
                return View("ShowAllApartments", new ApartmentVM { ListaApartmana = (List<Apartment>)_listOfAllApartments });
            }

            var PmtFreq = Request.Form["gradField"];


            List<Apartment> list = _listOfAllApartments.ToList().FindAll(x => x.TotalRooms >= filter.RoomNumber && x.MaxAdults >= filter.Adults
                                                        && x.MaxChildren >= filter.Children && x.CityId == filter.GradId);

            return Json(new {listApartments = list});
        }

        [HttpPost]
        public ActionResult SortApartments(RWA.Models.SortingApartment filter)
        {
            IList<DataLayer.Model.Apartment> _listOfAllApartments = RepoFactory.GetRepo().LoadApartments();
            var cities = RepoFactory.GetRepo().LoadCities();
            ViewBag.Cities = cities;


            switch (filter.Type)
            {
                case "id":
                    if (filter.AscDesc == 1)
                    {
                        _listOfAllApartments.ToList().Sort((x, y) => x.Id.CompareTo(y.Id));
                    }
                    else
                    {
                        _listOfAllApartments.ToList().Sort((x, y) => -x.Id.CompareTo(y.Id));
                    }
                    break;
                case "price":
                    if (filter.AscDesc == 1)
                    {
                        _listOfAllApartments.ToList().Sort((x, y) => x.Price.CompareTo(y.Price));
                    }
                    else
                    {
                        _listOfAllApartments.ToList().Sort((x, y) => -x.Price.CompareTo(y.Price));
                    }
                    break;
            }

            return Json(new { listApartments = _listOfAllApartments });
        }

        public ActionResult Odjava()
        {
            Session["user"] = null;

            return RedirectToAction("ShowAllApartments", "Home");
        }
    }
}