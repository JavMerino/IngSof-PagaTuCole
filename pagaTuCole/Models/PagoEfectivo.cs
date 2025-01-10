using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class PagoEfectivo
    {
        public string IdPagoEfectivo { get; set; }
        public decimal Monto { get; set; }
        public string Responsable { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; } = true;
        public string IdPago { get; set; }
    }
}