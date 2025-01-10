using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class Descuento
    {
        public string IdDescuento { get; set; }
        public string Descripcion { get; set; }
        public float Porcentaje { get; set; }
        public bool Estado { get; set; } = true;
    }
}