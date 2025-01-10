using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class Administrador
    {
        public string IdAdministrador { get; set; }
        public DateTime FecIngreso { get; set; }
        public string TipoDocumento { get; set; }
        public string NumDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Email { get; set; }
        public DateTime FecNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; } = true;
        public string Username { get; set; }
        public string Contraseña { get; set; }

        public string IdPersona { get; set; }
        public string IdUsuario { get; set; }
    }
}