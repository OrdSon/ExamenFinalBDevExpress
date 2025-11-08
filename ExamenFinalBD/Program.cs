using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using ExamenFinalBD.Tecnico;
namespace ExamenFinalBD
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());

        }
    }
}
