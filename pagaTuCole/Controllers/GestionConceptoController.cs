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
    public class GestionConceptoController : Controller
    {
        // GET: GestionConcepto
        public ActionResult GestionConcepto()
        {
            var conceptos = ObtenerConceptos();
            return View(conceptos);
        }

        // Método para obtener los conceptos activos de la base de datos
        private List<Concepto> ObtenerConceptos()
        {
            var conceptos = new List<Concepto>();
            string query = "pa_ListarConceptos";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conceptos.Add(new Concepto
                        {
                            IdConcepto = reader["id_concepto"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Importe = Convert.ToDecimal(reader["importe"]),
                            Estado = Convert.ToBoolean(reader["estado"]),
                            Nivel = reader["id_nivel"].ToString(),
                            Mes = reader["mes"].ToString()
                        });
                    }
                }
            }

            return conceptos;
        }

        // Acción para agregar un concepto (no implementada aún)
        [HttpPost]
        public ActionResult AgregarConcepto(Concepto model)
        {
            // Lógica para agregar concepto (por ahora no implementada)
            return RedirectToAction("GestionConcepto");
        }

        // Acción para editar un concepto (no implementada aún)
        [HttpPost]
        public ActionResult EditarConcepto(Concepto model)
        {
            // Lógica para editar concepto (por ahora no implementada)
            return RedirectToAction("GestionConcepto");
        }

        // Acción para eliminar un concepto (no implementada aún)
        [HttpPost]
        public ActionResult EliminarConcepto(string idConcepto)
        {
            // Lógica para eliminar concepto (por ahora no implementada)
            return RedirectToAction("GestionConcepto");
        }
    }
}
