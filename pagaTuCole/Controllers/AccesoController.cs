using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pagaTuCole.Models;
using System.Data;
using System.Data.SqlClient;


    namespace pagaTuCole.Controllers
    {
        public class AccesoController : Controller
        {
            // GET: Acceso
            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]

            public ActionResult Login(Usuario oUsuario)
            {
                using (SqlConnection cn = new SqlConnection(SqlConection.CadenaConexion))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("pa_ValidarUsuario", cn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@username", oUsuario.Username);
                        cmd.Parameters.AddWithValue("@contraseña", oUsuario.Contraseña);

                        // Abrir conexión
                        cn.Open();

                        // Ejecutar y leer los resultados
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Obtener el idUsuario y rol
                                oUsuario.IdUsuario = reader["idUsuario"].ToString();
                                bool rol = reader["rol"] != DBNull.Value && Convert.ToBoolean(reader["rol"]);

                                Session["usuario"] = oUsuario;

                                if (oUsuario.IdUsuario != "")
                                {
                                    if (!rol) // Administrador
                                    {
                                        ViewBag.Layout = "~/Views/Shared/_AdminLayout.cshtml";
                                        return RedirectToAction("GestionApoderado", "GestionApoderado");
                                    }
                                    else // Apoderado
                                    {
                                        ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
                                        return RedirectToAction("Index", "Home");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewData["Mensaje"] = "Error al iniciar sesión: " + ex.Message;
                    }
                }

                // Si no es válido o ocurre un error
                ViewData["Mensaje"] = "Usuario no encontrado o credenciales inválidas";
                return View();
            }


        }
    }