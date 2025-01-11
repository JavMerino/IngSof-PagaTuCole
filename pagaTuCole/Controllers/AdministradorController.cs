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
                            IdTipoDocumento = reader["id_tipoDocumento"].ToString(),
                            NumDocumento = reader["numero_documento"].ToString(),
                            TipoDocumento = reader["tipo_documento"].ToString(),
                            Email = reader["email"]?.ToString() ?? "No especificado",
                            
                            FecNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                            Ocupacion = reader["ocupacion"]?.ToString() ?? "No especificado",
                            Direccion = reader["direccion"].ToString(),
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

        [HttpPost]
        public ActionResult AgregarApoderado(Apoderado model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_insertar_tApoderado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                    cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                    cmd.Parameters.AddWithValue("@id_tipoDocumento", model.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                    cmd.Parameters.AddWithValue("@direccion", model.Direccion);
                    cmd.Parameters.AddWithValue("@teleContacto", model.TeleContacto);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@fecNacimiento", model.FecNacimiento);
                    cmd.Parameters.AddWithValue("@ocupacion", model.Ocupacion);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GestionApoderado");
        }

        [HttpPost]
        public ActionResult EditarApoderado(Apoderado model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_EditarApoderado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idApoderado", model.IdApoderado);
                    cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                    cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                    cmd.Parameters.AddWithValue("@id_tipoDocumento", model.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@direccion", model.Direccion);
                    cmd.Parameters.AddWithValue("@teleContacto", model.TeleContacto);
                    cmd.Parameters.AddWithValue("@fecNacimiento", model.FecNacimiento);
                    cmd.Parameters.AddWithValue("@ocupacion", model.Ocupacion);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GestionApoderado");
        }

        [HttpPost]
        public ActionResult CambiarEstadoApoderado(string idApoderado)
        {
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("pa_CambiarEstadoApoderado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idApoderado", idApoderado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("GestionApoderado");
        }

    }
}