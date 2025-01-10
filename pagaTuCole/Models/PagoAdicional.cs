using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class PagoAdicional
    {
        public string IdPagoAdicional { get; set; }
        public decimal ImporteTotal { get; set; } //suma de los importes del concepto filtrados por apoderado, concepto y mes
        public bool EstadoPago { get; set; } = false; // 0: pendiente, 1: pagado
        public string IdConcepto { get; set; }
        public string IdApoderado { get; set; }
    }
}