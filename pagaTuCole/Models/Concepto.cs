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
        public string IdNivel { get; set; }
        public string Mes { get; set; }

        
        public string NombreMes => MapeoMes(Mes);
        private string MapeoMes(string mes)
        {
            switch (mes)
            {
                case "01": return "Enero";
                case "02": return "Febrero";
                case "03": return "Marzo";
                case "04": return "Abril";
                case "05": return "Mayo";
                case "06": return "Junio";
                case "07": return "Julio";
                case "08": return "Agosto";
                case "09": return "Septiembre";
                case "10": return "Octubre";
                case "11": return "Noviembre";
                case "12": return "Diciembre";
                default: return "Desconocido";
            }
        }
    }
}