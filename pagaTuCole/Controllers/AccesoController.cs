using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pagaTuCole.Models;
using System.Data;
using System.Data.SqlClient;


namespace pagaTuCole.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=MERINO\\SQLEXPRESS;Initial Catalog=pruebaUsuario;Integrated Security=true";


        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Usuario oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("pa_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Username", oUsuario.Username);
                cmd.Parameters.AddWithValue("Contraseña", oUsuario.Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (oUsuario.IdUsuario != 0)
            {

                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
        }
    }
}