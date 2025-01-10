using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace pagaTuCole.Models
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string Username { get; set; }
        public string Contraseña { get; set; }
        public bool Rol { get; set; } // 0: admin, 1: apoderado
        public string IdPersona { get; set; }
        public int estado { get; set; }
    }
}