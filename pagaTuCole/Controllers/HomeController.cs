using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class HomeController : Controller
    {
        

        
        // Método principal para mostrar el dashboard del apoderado
        public ActionResult Index()
        {
            // Obtener el usuario desde la sesión
            var usuario = Session["usuario"] as Usuario;

            if (usuario == null)
            {
                return RedirectToAction("Login", "Acceso");
            }

            var datosApoderado = ObtenerDatosApoderado(usuario.IdUsuario);

            return View(datosApoderado);
        }

        // Método para obtener información del apoderado y sus alumnos
        private Apoderado ObtenerDatosApoderado(string idUsuario)
        {
            var datos = new Apoderado();

            string query = "pa_ObtenerDatosApoderado";

            using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Asignar datos obtenidos del procedimiento
                        datos.Nombres = $"{reader["nombres"]}";
                        datos.ApPaterno = reader["apPaterno"].ToString();
                        datos.ApMaterno = reader["apMaterno"].ToString();
                        datos.Email = reader["email"].ToString();
                        datos.Telefono = reader["telefono"].ToString();
                        datos.Direccion = reader["direccion"].ToString();
                        datos.Email = reader["email"].ToString();
                        datos.FecNacimiento = Convert.ToDateTime(reader["fecNacimiento"]);
                        datos.TipoDocumento = reader["tipo_documento"].ToString();
                        datos.NumDocumento = reader["numero_documento"].ToString();
                        datos.IdApoderado = reader["id_apoderado"].ToString() ;
                        datos.Ocupacion = reader["ocupacion"].ToString();
                        datos.TeleContacto = reader["teleContacto"].ToString();
                        datos.CantidadAlumnos = Convert.ToInt32(reader["cantidad_alumnos"]);
                    }
                }
            }

            return datos;
        }


        public ActionResult About()
        {
            var usuario = Session["usuario"] as Usuario;

            if (usuario == null)
            {
                return RedirectToAction("Login", "Acceso");
            }

            // Obtener información del apoderado
            var apoderado = ObtenerDatosApoderado(usuario.IdUsuario);

            // Obtener alumnos a cargo
            var alumnos = ObtenerAlumnosPorApoderado(apoderado.IdApoderado);

            // Crear el ViewModel
            var viewModel = new Apoderado
            {
                Nombres = apoderado.Nombres,
                ApPaterno = apoderado.ApPaterno,
                ApMaterno = apoderado.ApMaterno,
                TipoDocumento = apoderado.TipoDocumento,
                NumDocumento = apoderado.NumDocumento,
                TeleContacto = apoderado.TeleContacto,
                Email = apoderado.Email,
                Direccion = apoderado.Direccion,
                Alumnos = alumnos
            };

            return View(viewModel);
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
                            NumDocumento = reader["numero_documento"].ToString() ,
                            Email = reader["email_alumno"].ToString(),
                            Telefono = reader["telefono_alumno"].ToString(),
                            Direccion = reader["direccion_alumno"].ToString(),
                            FecNacimiento = Convert.ToDateTime(reader["fecha_nacimiento_alumno"]),
                            Descuento = reader["descuento"] == DBNull.Value ? "No especificado" : reader["descuento"].ToString(),
                                porcentajeDescuento = reader["porcentaje_descuento"] != DBNull.Value? Convert.ToDouble(reader["porcentaje_descuento"]): 0.0,
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
        public ActionResult Contact()
        {
            // Obtener el usuario desde la sesión
            var usuario = Session["usuario"] as Usuario;

            if (usuario == null)
            {
                return RedirectToAction("Login", "Acceso");
            }

            var datosApoderado = ObtenerDatosApoderado(usuario.IdUsuario);

            return View(datosApoderado);
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");
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