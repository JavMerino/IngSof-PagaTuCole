using System;
using System.Collections.Generic;
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
        public ActionResult GestionApoderado()
        {
            var apoderados = ObtenerApoderados();
            return View(apoderados);
        }

        private List<Apoderado> ObtenerApoderados()
        {
            var apoderados = new List<Apoderado>();

            string query = "pa_ListarApoderados";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        apoderados.Add(new Apoderado
                        {
                            IdApoderado = reader["id_apoderado"].ToString(),
                            IdPersona = reader["id_persona"].ToString(),
                            Nombres = reader["nombres"].ToString(),
                            ApPaterno = reader["apPaterno"].ToString(),
                            ApMaterno = reader["apMaterno"].ToString(),
                            NumDocumento = reader["numero_documento"].ToString(),
                            TipoDocumento = reader["tipo_documento"].ToString(),
                            Email = reader["email"]?.ToString() ?? "No especificado",
                            FecNacimiento = reader["fecha_nacimiento"] != DBNull.Value
                                              ? Convert.ToDateTime(reader["fecha_nacimiento"])
                                              : DateTime.MinValue,
                            Ocupacion = reader["ocupacion"]?.ToString() ?? "No especificado",
                            TeleContacto = reader["teleContacto"]?.ToString() ?? "No especificado",
                            Estado = Convert.ToBoolean(reader["estado_apoderado"]),
                            Username = reader["username"].ToString(),
                            Contraseña = reader["contraseña"].ToString(),
                        });
                    }
                }
            }

            return apoderados;
        }
    }
}