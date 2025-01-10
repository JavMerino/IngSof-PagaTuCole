using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class Pago
    {
        public string IdPago { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal Mora { get; set; } = 0.00M;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string TipoPago { get; set; }
        public bool Estado { get; set; } = false; // 0: pendiente, 1: pagado
        public string IdPensionEnseñanza { get; set; }
        public string IdPagoAdicional { get; set; }
    }
}