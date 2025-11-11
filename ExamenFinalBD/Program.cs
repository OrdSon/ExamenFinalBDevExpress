using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalBD
{
    internal static class Program
    {
        /// <summary>
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

            

            var cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            using (var cn = new SqlConnection(cs))
            {
                cn.Open(); 
            }


            Application.Run(new AdminHome());

        }
    }
}
