using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ExamenFinalBD.Utils
{
    public class UIUtils
    {
        /// <summary>
        /// Centra un control dentro de su contenedor padre.
        /// </summary>

        public void CenterControl(Control controlToCenter, Form parentContainer)
        {
            if(controlToCenter == null || parentContainer == null)
            {
                throw new ArgumentNullException("El control o el contenedor padre no pueden ser nulos.");
            }
            int x = (parentContainer.Width - controlToCenter.Width) / 2;

            int y = (parentContainer.Height - controlToCenter.Height) / 2;

            controlToCenter.Location = new Point(x, y);
            MessageBox.Show(x + " " + y);
        }
    }
}
