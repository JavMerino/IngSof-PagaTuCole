using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pagaTuCole.Models;

namespace pagaTuCole.Controllers
{
    public class GestionDescuentoController : Controller
    {
        // GET: GestionDescuento
        public ActionResult GestionDescuento()
        {
            var descuentos = ObtenerDescuentos();
            return View(descuentos);
        }

        // Método para obtener los descuentos activos de la base de datos
        private List<Descuento> ObtenerDescuentos()
        {
            var descuentos = new List<Descuento>();
            string query = "pa_ListarDescuentos";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        descuentos.Add(new Descuento
                        {
                            IdDescuento = reader["id_descuento"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Porcentaje = Convert.ToSingle(reader["porcentaje"]),
                            Estado = Convert.ToBoolean(reader["estado"])
                        });
                    }
                }
            }

            return descuentos;
        }

        // Acción para agregar un descuento (no implementada aún)
        [HttpPost]
        public ActionResult AgregarDescuento(Descuento model)
        {
            // Lógica para agregar descuento (por ahora no implementada)
            return RedirectToAction("GestionDescuento");
        }

        // Acción para editar un descuento (no implementada aún)
        [HttpPost]
        public ActionResult EditarDescuento(Descuento model)
        {
            // Lógica para editar descuento (por ahora no implementada)
            return RedirectToAction("GestionDescuento");
        }

        // Acción para eliminar un descuento (no implementada aún)
        [HttpPost]
        public ActionResult EliminarDescuento(string idDescuento)
        {
            // Lógica para eliminar descuento (por ahora no implementada)
            return RedirectToAction("GestionDescuento");
        }
    }
}
