﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace pagaTuCole
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GestionApoderado",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GestionApoderado", action = "GestionApoderado", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GestionAlumno",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GestionAlumno", action = "GestionAlumno", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GestionAdministrador",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GestionAdministrador", action = "GestionAdministrador", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GestionDescuento",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GestionDescuento", action = "GestionDescuento", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GestionConcepto",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GestionConcepto", action = "GestionConcepto", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Administrador",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Administrador", action = "inicio", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
