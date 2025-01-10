using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class Alumno
    {
        public string IdAlumno { get; set; }
        public DateTime FecIngreso { get; set; }
        public string Grado { get; set; }
        public string Nivel { get; set; }
        public float Descuento { get; set; } = 0;
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

        public string IdPersona { get; set; }
    }
}