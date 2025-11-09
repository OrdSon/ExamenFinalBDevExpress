using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalBD.Tecnico.BLL
{
    public class General
    {
        //para los datos del Contrato
        public string NoContrato { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        // para los repuestos
        public int IdRepuesto { get; set; }
        public string Repuesto {  get; set; }
        public decimal Precio { get; set; } 
    }
}
