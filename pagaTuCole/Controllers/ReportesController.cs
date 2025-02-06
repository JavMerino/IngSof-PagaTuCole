using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pagaTuCole.Models;

namespace pagaTuCole.Controllers
{
    public class ReportesController : Controller
    {
        // Acción para mostrar los reportes
        public ActionResult Reportes()
        {
            return View(); // Aquí se carga la vista "Reportes.cshtml" que contiene los enlaces a los reportes
        }

        // Acción para generar el reporte de Alumnos por Nivel y Grado
        public ActionResult ReporteAlumnosPorNivelGrado()
        {
            var reporte = ObtenerReporteAlumnosPorNivelGrado();
            return View(reporte);
        }

        private List<ReporteAlumnos> ObtenerReporteAlumnosPorNivelGrado()
        {
            var alumnos = new List<ReporteAlumnos>();

            string query = "pa_ReporteAlumnosPorNivelGrado"; // Llama al SP correspondiente

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        alumnos.Add(new ReporteAlumnos
                        {
                            Nivel = reader["nivel_nombre"].ToString(),
                            Grado = reader["grado"].ToString(),
                            CantidadAlumnos = Convert.ToInt32(reader["cantidad_alumnos"])
                        });
                    }
                }
            }

            return alumnos;
        }

        // Acción para generar el reporte de Apoderados con Cantidad de Alumnos a Cargo
        public ActionResult ReporteApoderadosConAlumnos()
        {
            var reporte = ObtenerReporteApoderadosConAlumnos();
            return View(reporte);
        }

        private List<ReporteApoderados> ObtenerReporteApoderadosConAlumnos()
        {
            var apoderados = new List<ReporteApoderados>();

            string query = "pa_ReporteApoderadosConAlumnos"; // Llama al SP correspondiente

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        apoderados.Add(new ReporteApoderados
                        {
                            NombreApoderado = reader["NombreApoderado"].ToString(),
                            CantidadAlumnos = Convert.ToInt32(reader["CantidadAlumnos"])
                        });
                    }
                }
            }

            return apoderados;
        }

        // Acción para generar el reporte de Pagos
        public ActionResult ReportePagos()
        {
            var reporte = ObtenerReportePagos();
            return View(reporte);
        }

        private List<ReportePagos> ObtenerReportePagos()
        {
            var pagos = new List<ReportePagos>();

            string query = "pa_ReportePagos"; // Nombre del stored procedure

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pagos.Add(new ReportePagos
                        {
                            IdPago = reader["ID_Pago"].ToString(),
                            Fecha = reader["Fecha_Pago"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Pago"]) : DateTime.MinValue,
                            TipoPago = reader["Tipo_Pago"].ToString(),
                            ImporteTotal = Convert.ToDecimal(reader["Importe_Total"]),
                            EstadoPago = reader["Estado_Pago"].ToString(),
                            NombreAlumno = reader["Nombre_Alumno"].ToString(),
                            NombreApoderado = reader["Nombre_Apoderado"].ToString(),
                            MesPension = reader["Mes_Pension"].ToString(),
                            ConceptoAdicional = reader["Concepto_Adicional"]?.ToString() ?? "N/A"
                        });
                    }
                }
            }

            return pagos;
        }
    }


    // Clase para el modelo de Reporte de Pagos
    public class ReportePagos
    {
        public string IdPago { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoPago { get; set; }
        public decimal ImporteTotal { get; set; }
        public string EstadoPago { get; set; }
        public string NombreAlumno { get; set; }
        public string NombreApoderado { get; set; }
        public string MesPension { get; set; }
        public string ConceptoAdicional { get; set; }
    }

    public class ReporteAlumnos
    {
        public string Nivel { get; set; }
        public string Grado { get; set; }
        public int CantidadAlumnos { get; set; }
    }

    public class ReporteApoderados
    {
        public string NombreApoderado { get; set; }
        public int CantidadAlumnos { get; set; }
    }


}