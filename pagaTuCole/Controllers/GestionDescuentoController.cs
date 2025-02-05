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
            // Validar datos
            if (!ModelState.IsValid)
            {
                // En caso de error de validación, podemos recargar la vista con la lista y el modelo
                var lista = ObtenerDescuentos();
                return View("GestionDescuento", lista);
            }

            // Llamamos al SP "pa_AgregarDescuento"
            string sp = "pa_AgregarDescuento";
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(sp, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@descripcion", model.Descripcion);
                cmd.Parameters.AddWithValue("@porcentaje", model.Porcentaje);

                cn.Open();
                cmd.ExecuteNonQuery(); // Ejecuta el insert
            }

            return RedirectToAction("GestionDescuento");
        }

        // Acción para editar un descuento
        [HttpPost]
        public ActionResult EditarDescuento(Descuento model)
        {
            // Validar datos
            if (!ModelState.IsValid)
            {
                // Retornar la misma vista con la data
                var lista = ObtenerDescuentos();
                return View("GestionDescuento", lista);
            }

            // Llamamos al SP "pa_EditarDescuento"
            string sp = "pa_EditarDescuento";
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(sp, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idDescuento", model.IdDescuento);
                cmd.Parameters.AddWithValue("@descripcion", model.Descripcion);
                cmd.Parameters.AddWithValue("@porcentaje", model.Porcentaje);

                cn.Open();
                cmd.ExecuteNonQuery(); // Ejecuta el update
            }

            return RedirectToAction("GestionDescuento");
        }

        // Acción para eliminar (desactivar) un descuento
        [HttpPost]
        public ActionResult EliminarDescuento(string idDescuento)
        {
            // Llamamos al SP "pa_EliminarDescuento"
            string sp = "pa_EliminarDescuento";
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(sp, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idDescuento", idDescuento);

                cn.Open();
                cmd.ExecuteNonQuery(); // UPDATE ... estado=0
            }

            return RedirectToAction("GestionDescuento");
        }
    }
}

