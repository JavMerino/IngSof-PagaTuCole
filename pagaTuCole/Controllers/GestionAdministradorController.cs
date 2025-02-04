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
    public class GestionAdministradorController : Controller
    {
        // GET: GestionAdministrador
        public ActionResult GestionAdministrador()
        {
            var administradores = ObtenerAdministradores();
            return View(administradores);
        }

        // Método para obtener los administradores activos desde la base de datos
        private List<Administrador> ObtenerAdministradores()
        {
            var administradores = new List<Administrador>();
            string query = "pa_ListarAdministradores";  // Procedimiento almacenado para obtener administradores

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        administradores.Add(new Administrador
                        {
                            IdAdministrador = reader["id_administrador"].ToString(),
                            Nombres = reader["nombres"].ToString(),
                            ApPaterno = reader["apPaterno"].ToString(),
                            ApMaterno = reader["apMaterno"].ToString(),
                            NumDocumento = reader["numero_documento"].ToString(),
                            TipoDocumento = reader["tipo_documento"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : string.Empty, // Protección contra DBNull
                            FecIngreso = reader["fecIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["fecIngreso"]) : DateTime.MinValue,

                            Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : string.Empty,  // Protección para dirección
                            Estado = reader["estado_administrador"].ToString() == "1",
                            //FecIngreso = Convert.ToDateTime(reader["fecIngreso"]),
                            Username = reader["username"].ToString(),
                            Contraseña = reader["contraseña"].ToString(),
                            IdPersona = reader["id_persona"].ToString(),
                            IdUsuario = reader["id_usuario"].ToString()
                        });

                    }
                }
            }

            return administradores;
        }

        // Acción para agregar un administrador (no implementada aún)
        [HttpPost]
        public ActionResult AgregarAdministrador(Administrador model)
        {
            // Lógica para agregar administrador (por ahora no implementada)
            return RedirectToAction("GestionAdministrador");
        }

        // Acción para editar un administrador (no implementada aún)
        [HttpPost]
        public ActionResult EditarAdministrador(Administrador model)
        {
            // Lógica para editar administrador (por ahora no implementada)
            return RedirectToAction("GestionAdministrador");
        }

        // Acción para eliminar un administrador (no implementada aún)
        [HttpPost]
        public ActionResult EliminarAdministrador(string idAdministrador)
        {
            // Lógica para eliminar administrador (por ahora no implementada)
            return RedirectToAction("GestionAdministrador");
        }
    }
}
