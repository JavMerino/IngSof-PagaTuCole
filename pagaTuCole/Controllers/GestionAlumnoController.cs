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

    public class GestionAlumnoController : Controller
    {
        // GET: GestionAlumno
        public ActionResult GestionAlumno()
        {
            var alumnos = ObtenerAlumnos();
            var descuentos = ObtenerDescuentos();
            ViewBag.ListaDescuentos = descuentos;
            return View(alumnos);
        }

        // Método para obtener los alumnos de la base de datos
        private List<Alumno> ObtenerAlumnos()
        {
            var alumnos = new List<Alumno>();
            string query = "pa_ListarAlumnos";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        alumnos.Add(new Alumno
                        {
                            IdAlumno = reader["id_alumno"].ToString(),
                            IdPersona = reader["id_persona"].ToString(),
                            
                            FecIngreso = Convert.ToDateTime(reader["fecIngreso"]),
                            Grado = reader["grado"].ToString(),
                            IdNivel = reader["id_nivel"].ToString(),
                            Nivel = reader["nivel"].ToString(),  // Aquí se asume que 'nivel' es un char(1)
                            IdDescuento = reader["id_descuento"].ToString(),
                            Descuento = reader["descuento"].ToString(),
                            Estado = Convert.ToBoolean(reader["estado"]),
                            Nombres = reader["nombre_alumno"].ToString(),
                            ApPaterno = reader["apellido_paterno"].ToString(),
                            ApMaterno = reader["apellido_materno"].ToString(),
                            IdTipoDocumento = reader["id_tipoDocumento"].ToString(),
                            TipoDocumento = reader["tipo_documento"].ToString(),
                            NumDocumento = reader["numero_documento"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            FecNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"])
                        });
                    }
                }
            }

            return alumnos;
        }


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

        // Acción para agregar un alumno
        [HttpPost]
        public ActionResult AgregarAlumno(Alumno model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_InsertarAlumno", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros según el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@id_tipoDocumento", model.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                    cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                    cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                    cmd.Parameters.AddWithValue("@email", (object)model.Email ?? DBNull.Value); // Permitir NULL
                    cmd.Parameters.AddWithValue("@fecNacimiento", model.FecNacimiento);
                    cmd.Parameters.AddWithValue("@telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", model.Direccion); // Permitir NULL
                    cmd.Parameters.AddWithValue("@grado", model.Grado);
                    cmd.Parameters.AddWithValue("@id_nivel", model.IdNivel);
                    cmd.Parameters.AddWithValue("@id_descuento", (object)model.IdDescuento ?? DBNull.Value); // Permitir NULL

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GestionAlumno");
        }

        // Acción para editar un alumno
        [HttpPost]
        public ActionResult EditarAlumno(Alumno model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_EditarAlumno", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@idAlumno", model.IdAlumno);
                    cmd.Parameters.AddWithValue("@idPersona", model.IdPersona);
                    cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                    cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                    cmd.Parameters.AddWithValue("@idTipoDocumento", model.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                    cmd.Parameters.AddWithValue("@direccion", model.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("@grado", model.Grado);
                    cmd.Parameters.AddWithValue("@idNivel", model.IdNivel);
                    cmd.Parameters.AddWithValue("@idDescuento", model.IdDescuento);
                    cmd.Parameters.AddWithValue("@estado", model.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GestionAlumno");
        }

        // Acción para eliminar un alumno
        [HttpPost]
        public ActionResult EliminarAlumno(string idAlumno)
        {
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                // Llamar al procedimiento almacenado para eliminar (cambiar estado) del alumno
                SqlCommand cmd = new SqlCommand("pa_EliminarAlumno", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetro del SP
                cmd.Parameters.AddWithValue("@id_alumno", idAlumno);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            // Redirigir a la vista de gestión de alumnos
            return RedirectToAction("GestionAlumno");
        }
    }
}
