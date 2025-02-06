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

    public class GestionApoderadoController : Controller
    {
        // GET: GestionApoderado
        
        public ActionResult GestionApoderado()
        {
            var apoderados = ObtenerApoderados();
            return View(apoderados);
        }

        [HttpGet]
        public ActionResult ConsultarAlumnos(string idApoderado)
        {
            // 1. Obtener la lista de alumnos de ese apoderado:
            var listaAlumnos = ObtenerAlumnosPorApoderado(idApoderado);
            var listaAlumnosDisponibles= ObtenerAlumnosDisponibles();

            ViewBag.disponibles = listaAlumnosDisponibles;

            ViewBag.IdApoderado = idApoderado;
            // O la lógica que corresponda a tu DB (stored procedure, etc.)

            // 2. Retornar la vista con esa lista   
            return View(listaAlumnos);
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
                            FecNacimiento = reader["fecha_nacimiento"] != DBNull.Value
                            ? Convert.ToDateTime(reader["fecha_nacimiento"])
                            : DateTime.MinValue,  // Aseguramos que no sea null
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
                    cmd.Parameters.AddWithValue("@ocupacion", model.Ocupacion);
                    cmd.Parameters.AddWithValue("@teleContacto", model.TeleContacto);
                    cmd.Parameters.AddWithValue("@estado", model.Estado);
                    cmd.Parameters.AddWithValue("@idPersona", model.IdPersona);
                    cmd.Parameters.AddWithValue("@nombres", model.Nombres);
                    cmd.Parameters.AddWithValue("@apPaterno", model.ApPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", model.ApMaterno);
                    cmd.Parameters.AddWithValue("@idTipoDocumento", model.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@numDocumento", model.NumDocumento);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@direccion", model.Direccion);
                    cmd.Parameters.AddWithValue("@fecNacimiento", model.FecNacimiento);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GestionApoderado");
        }

        [HttpPost]
        public ActionResult EliminarApoderado(string idApoderado)
        {
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                // Aquí llamas a tu SP pa_EliminarApoderado (o como se llame)
                SqlCommand cmd = new SqlCommand("pa_EliminarApoderado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_apoderado", idApoderado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            // Puedes redirigir o retornar un JSON de éxito
            return RedirectToAction("GestionApoderado");
        }

        [HttpPost]
        public ActionResult EliminarRelacionApoderadoAlumno(string idApoderadoAlumno)
        {
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("pa_EliminarRelacionApoderadoAlumno", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_apoderadoAlumno", idApoderadoAlumno);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("GestionApoderado");
        }

        private List<Alumno> ObtenerAlumnosPorApoderado(string idApoderado)
        {
            var alumnos = new List<Alumno>();

            string query = "pa_ObtenerAlumnosPorIdApoderado";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_apoderado", idApoderado);

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            alumnos.Add(new Alumno
                            {
                                IdAlumno = reader["id_alumno"].ToString(),
                                IdApoderadoAlumno = reader["idApoderadoAlumno"].ToString(),
                                Nombres = $"{reader["nombre_alumno"]} {reader["apellido_paterno"]} {reader["apellido_materno"]}",
                                Grado = reader["grado"].ToString(),
                                TipoDocumento = reader["tipo_documento"].ToString(),
                                NumDocumento = reader["numero_documento"].ToString(),
                                Email = reader["email_alumno"].ToString(),
                                Telefono = reader["telefono_alumno"].ToString(),
                                Direccion = reader["direccion_alumno"].ToString(),
                                FecNacimiento = Convert.ToDateTime(reader["fecha_nacimiento_alumno"]),
                                Descuento = reader["descuento"] == DBNull.Value ? "No especificado" : reader["descuento"].ToString(),
                                porcentajeDescuento = reader["porcentaje_descuento"] != DBNull.Value ? Convert.ToDouble(reader["porcentaje_descuento"]) : 0.0,
                                FecIngreso = Convert.ToDateTime(reader["fecha_ingreso"]),
                                IdDescuento = reader["id_descuento"] == DBNull.Value ? "No especificado" : reader["id_descuento"].ToString(),
                                Mensualidad = Convert.ToDouble(reader["mensualidad"]),
                                Nivel = reader["nivel"].ToString(),
                                Parentesco = reader["parentesco"].ToString(),
                            });
                        }
                        catch (Exception ex)
                        {
                            // Loguear el error o inspeccionarlo
                            throw new Exception($"Error al procesar un registro: {ex.Message}");
                        }
                    }
                }
            }

            return alumnos;
        }

        private List<Alumno> ObtenerAlumnosDisponibles()
        {
            var alumnos = new List<Alumno>();

            string query = "pa_ListarAlumnosDisponibles";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            alumnos.Add(new Alumno
                            {
                                IdAlumno = reader["id_alumno"].ToString(),
                                Nombres = $"{reader["nombres"]} {reader["apPaterno"]} {reader["apMaterno"]}",
                                Grado = reader["grado"].ToString(),
                                NumDocumento = reader["numDocumento"].ToString()
                            });
                        }
                        catch (Exception ex)
                        {
                            // Loguear el error o inspeccionarlo
                            throw new Exception($"Error al procesar un registro: {ex.Message}");
                        }
                    }
                }
            }

            return alumnos;
        }

        public ActionResult AsignarAlumno(ApoderadoAlumno model)
        {
            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("pa_AsignarAlumnoApoderado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_alumno", model.IdAlumno);
                cmd.Parameters.AddWithValue("@id_apoderado", model.IdApoderado);
                cmd.Parameters.AddWithValue("@parentesco", model.Parentesco);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("ConsultarAlumnos", new { idApoderado = model.IdApoderado });
        }


        [HttpGet]
        public JsonResult ObtenerMensualidades(string IdApoderadoAlumno)
        {
            var mensualidades = new List<PensionEnseñanza>();

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("pa_ObtenerMensualidades", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_apoderadoAlumno", IdApoderadoAlumno);
                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mensualidades.Add(new PensionEnseñanza
                        {
                            IdPensionEnseñanza = reader["id_pensionEnseñanza"].ToString(),
                            IdApoderadoAlumno = reader["id_apoderadoAlumno"].ToString(),
                            ImporteTotal = Convert.ToDecimal(reader["importeTotal"]),
                            Mes = reader["mes"].ToString(),
                            Estado = Convert.ToInt32(reader["estado"]) // Asegúrate de agregar el estado en tu procedimiento almacenado
                        });
                    }
                }
            }

            // Convertir a JSON
            return Json(mensualidades.Select(m => new
            {
                IdPensionEnseñanza = m.IdPensionEnseñanza,
                IdApoderadoAlumno = m.IdApoderadoAlumno,
                ImporteTotal = m.ImporteTotal,
                Mes = m.Mes,
                Estado = m.Estado // 0: Pendiente, 1: Pagado
            }), JsonRequestBehavior.AllowGet);
        }

    }
}
