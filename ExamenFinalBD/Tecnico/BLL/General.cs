using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalBD.Tecnico.BLL
{
    public class General
    {
        public static string cadena = "Data Source=sql5113.site4now.net;Initial Catalog=db_ac0671_final;Persist Security Info=True;User ID=db_ac0671_final_admin;Password=examenfinal.1;Encrypt=False";
        //para los datos del Contrato
        public string NoContrato { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string EstadoContr {  get; set; }
        // para los repuestos
        public string IdRepuesto { get; set; }
        public string Repuesto {  get; set; }
        public decimal Precio { get; set; } 
    }
}
