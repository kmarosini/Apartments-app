using ApartmentsMVCApp.Models;
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
            
            return View(new ApartmentViewModel
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

    }
}