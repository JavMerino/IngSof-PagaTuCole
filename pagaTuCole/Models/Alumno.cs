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
        public string Descuento { get; set; }

        public Double porcentajeDescuento { get; set; } = 0.00;
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
        
        public double Mensualidad { get; set; }

        public string Parentesco { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string IdPersona { get; set; }
        public string IdApoderadoAlumno { get; set; }
        public string IdNivel { get; set; }
        public string IdTipoDocumento { get; set; }
        public string IdDescuento { get; set; }
    }
}