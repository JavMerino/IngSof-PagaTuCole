    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class ApoderadoAlumno
    {
        public string IdApoderadoAlumno { get; set; }
        public string IdAlumno { get; set; }
        public string IdApoderado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; } = true;
    }
}