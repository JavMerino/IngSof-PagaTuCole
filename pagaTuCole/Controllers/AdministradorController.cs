using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pagaTuCole.Models;
using pagaTuCole.Permisos;

namespace pagaTuCole.Controllers
{
    [ValidarSesion]
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult inicio()
        {
            return View();
        }
        

    }
}