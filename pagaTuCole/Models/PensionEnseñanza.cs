using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class PensionEnseñanza
    {
        public string IdPensionEnseñanza { get; set; }
        public string Mes { get; set; }
        public string IdApoderado { get; set; }
        public decimal Mensualidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImporteTotal { get; set; }
        public bool Estado { get; set; } = false; // 0: pendiente, 1: pagado
    }
}