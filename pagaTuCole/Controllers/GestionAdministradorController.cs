using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pagaTuCole.Models;
using pagaTuCole.Permisos;

namespace pagaTuCole.Controllers
{
    [ValidarSesion]

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
                            IdTipoDocumento = reader["id_tipoDocumento"].ToString(),
                            NumDocumento = reader["numero_documento"].ToString(),
                            TipoDocumento = reader["tipo_documento"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : string.Empty, // Protección contra DBNull
                            FecIngreso = reader["fecIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["fecIngreso"]) : DateTime.MinValue,

                            Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : string.Empty,  // Protección para dirección
                            Estado = Convert.ToBoolean(reader["estado_administrador"]),
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
            if (ModelState.IsValid) // Verifica que los datos del modelo sean válidos
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    try
                    {
                        // Llamar al procedimiento almacenado
                        SqlCommand cmd = new SqlCommand("pa_InsertarAdministrador", cn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Asignar parámetros al procedimiento almacenado
                        cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                        cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                        cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                        cmd.Parameters.AddWithValue("@idTipoDocumento", model.IdTipoDocumento);
                        cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                        cmd.Parameters.AddWithValue("@email", model.Email ?? (object)DBNull.Value); // Permitir valores nulos
                        cmd.Parameters.AddWithValue("@telefono", model.Telefono);
                        cmd.Parameters.AddWithValue("@fecNacimiento", model.FecNacimiento);
                        cmd.Parameters.AddWithValue("@direccion", model.Direccion);

                        // Abrir conexión y ejecutar el comando
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        ModelState.AddModelError("", "Error al agregar el administrador: " + ex.Message);
                        return View(model); // Retornar la vista con el modelo actual si ocurre un error
                    }
                }
            }
            return RedirectToAction("GestionAdministrador");
        }

        // Acción para editar un administrador (no implementada aún)
        [HttpPost]
        public ActionResult EditarAdministrador(Administrador model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_EditarAdministrador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_administrador", model.IdAdministrador);
                    cmd.Parameters.AddWithValue("@id_persona", model.IdPersona);
                    cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                    cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                    cmd.Parameters.AddWithValue("@id_tipoDocumento", model.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                    cmd.Parameters.AddWithValue("@email", model.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", model.Direccion);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("GestionAdministrador");
            }

            // Si los datos no son válidos, redirigir o retornar un mensaje de error
            return RedirectToAction("GestionAdministrador");
        }

        // Acción para eliminar un administrador (no implementada aún)
        [HttpPost]
        public ActionResult EliminarAdministrador(string idAdministrador, string idPersona)
        {
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("pa_EliminarAdministrador", cn);
                
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idAdministrador", idAdministrador);
                    cmd.Parameters.AddWithValue("@idPersona", idPersona);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                
            }
            return RedirectToAction("GestionAdministrador");
        }
    }
}
