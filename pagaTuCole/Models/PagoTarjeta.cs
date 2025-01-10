using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagaTuCole.Models
{
    public class PagoTarjeta
    {
        public string IdPagoTarjeta { get; set; }
        public decimal Monto { get; set; }
        public string NumTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serial { get; set; }

        public string IdPago { get; set; }
    }
}