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
            Application.SetCompatibleTextRenderingDefault(true);
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            DevExpress.XtraEditors.WindowsFormsSettings.SetPerMonitorDpiAware();
            DevExpress.XtraEditors.WindowsFormsSettings.AllowDpiScale = true;
            DevExpress.XtraEditors.WindowsFormsSettings.EnableFormSkins();

            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
            AppearanceObject.DefaultFont = new Font("Microsoft JhengHei UI", 8);

            Application.Run(new  Cajero());

        }
    }
}
