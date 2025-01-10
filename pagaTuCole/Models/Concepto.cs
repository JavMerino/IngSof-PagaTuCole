using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class Concepto
    {
        public string IdConcepto { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public bool Estado { get; set; } = true;
        public string Nivel { get; set; }
        public string Mes { get; set; }
    }
}