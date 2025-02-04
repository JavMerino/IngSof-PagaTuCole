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
    public class GestionAlumnoController : Controller
    {
        // GET: GestionAlumno
        public ActionResult GestionAlumno()
        {
            var alumnos = ObtenerAlumnos();
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
                            FecIngreso = Convert.ToDateTime(reader["fecIngreso"]),
                            Grado = reader["grado"].ToString(),
                            Nivel = reader["nivel"].ToString(),  // Aquí se asume que 'nivel' es un char(1)
                            Descuento = reader["descuento"].ToString(),
                            Estado = Convert.ToBoolean(reader["estado"]),
                            Nombres = reader["nombre_alumno"].ToString(),
                            ApPaterno = reader["apellido_paterno"].ToString(),
                            ApMaterno = reader["apellido_materno"].ToString(),
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

        // Acción para agregar un alumno
        [HttpPost]
        public ActionResult AgregarAlumno(Alumno model)
        {
            if (ModelState.IsValid)
            {
                // Verifica que Nivel sea un solo carácter
                if (model.Nivel.Length != 1)
                {
                    ModelState.AddModelError("Nivel", "El nivel debe ser un solo carácter.");
                    return View(model);
                }

                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_InsertarAlumno", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecIngreso", model.FecIngreso);
                    cmd.Parameters.AddWithValue("@grado", model.Grado);
                    cmd.Parameters.AddWithValue("@idNivel", model.Nivel);  // Asegúrate de que este valor es un solo carácter
                    cmd.Parameters.AddWithValue("@idDescuento", model.IdDescuento);
                    cmd.Parameters.AddWithValue("@idPersona", model.IdPersona);
                    cmd.Parameters.AddWithValue("@estado", model.Estado);

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
                // Verifica que Nivel sea un solo carácter
                if (model.Nivel.Length != 1)
                {
                    ModelState.AddModelError("Nivel", "El nivel debe ser un solo carácter.");
                    return View(model);
                }

                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("pa_EditarAlumno", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idAlumno", model.IdAlumno);
                    cmd.Parameters.AddWithValue("@fecIngreso", model.FecIngreso);
                    cmd.Parameters.AddWithValue("@grado", model.Grado);
                    cmd.Parameters.AddWithValue("@idNivel", model.Nivel);  // Asegúrate de que este valor es un solo carácter
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
                SqlCommand cmd = new SqlCommand("pa_EliminarAlumno", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("GestionAlumno");
        }
    }
}
