﻿using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ApartmentsApp
{
    public class Global : HttpApplication
    {
        private readonly IRepo _repo;

        public Global()
        {
            _repo = RepoFactory.GetRepo();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["database"] = _repo;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}