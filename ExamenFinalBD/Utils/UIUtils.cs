using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
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

        public bool validarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            try
            {
                return Regex.IsMatch(email, patronEmail, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool validarTxtEditEmail(TextEdit controlEmail, ErrorProvider errorProvider)
        {
            string emailIngresado = controlEmail.Text.Trim();
            bool esValido = validarEmail(emailIngresado);

            if (!esValido)
            {
                errorProvider.SetError(controlEmail, "Formato de correo electrónico no válido. (ejemplo@dominio.com)");
            }
            else
            {
                errorProvider.SetError(controlEmail, "");
            }

            return esValido;
        }

        public bool validarNoVacio(TextEdit control, ErrorProvider errorProvider, string nombreCampo)
        {
            string textoIngresado = control.Text.Trim();
            bool esValido = !string.IsNullOrEmpty(textoIngresado);

            if (!esValido)
            {
                errorProvider.SetError(control, $"🚨 El campo '{nombreCampo}' no puede estar vacío.");
            }
            else
            {
                errorProvider.SetError(control, "");
            }

            return esValido;
        }

        public void clearTextEdits(params TextEdit[] controles)
        {
            foreach (var control in controles)
            {
                control.Text = string.Empty;
            }
        }
        public bool validarLongitudMaxima(TextEdit control, ErrorProvider errorProvider, int maxLongitud, string nombreCampo)
        {
            string textoIngresado = control.Text ?? string.Empty;
            bool esValido = textoIngresado.Length <= maxLongitud;

            if (!esValido)
            {
                errorProvider.SetError(control, $"🚨 El campo '{nombreCampo}' excede el límite de {maxLongitud} caracteres.");
            }
            else
            {
                errorProvider.SetError(control, "");
            }

            return esValido;
        }

        public void restringirSoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false; 
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;             }
            else
            {
                e.Handled = true;
            }
        }
    }
}
