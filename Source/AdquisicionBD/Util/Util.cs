using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Util
{
    public class Util
    {
        public static Form GetActiveForm()
        {
            Form activeForm = Form.ActiveForm;

            //  Returns null for an MDI app            
            if (activeForm == null)
            {
                FormCollection openForms = Application.OpenForms;
                for (int i = 0; i < openForms.Count && activeForm == null; ++i)
                {
                    Form openForm = openForms[i];
                    if (openForm.IsMdiContainer)
                    {
                        activeForm = openForm.ActiveMdiChild;
                        break;
                    }
                }
            }
            else if (activeForm.IsMdiContainer && (activeForm.ActiveMdiChild != null))
            {
                activeForm = activeForm.ActiveMdiChild;
            }

            return activeForm;
        }
		
		public static DialogResult ShowDialog(Form owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            DialogResult resultado;
            bool previsualizacionKeys = owner.KeyPreview;
            owner.KeyPreview = false;
            resultado = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton);
            owner.KeyPreview = previsualizacionKeys;
            return resultado;
        }
		
		public static double Truncate(double Valor, int decimales)
        {

           return Math.Truncate(Valor * (10 * decimales))/(10 * decimales);
        }

        public static DateTime GetHoraSinVeranoToLocal(DateTime Hora)
        {
            if (TimeZoneInfo.Local.IsDaylightSavingTime(Hora))
              return Hora.AddHours(1);
           else
              return Hora;
        }

        public static DateTime GetHoraLocalToSinVerano(DateTime Hora)
        {
            if (TimeZoneInfo.Local.IsDaylightSavingTime(Hora))
                return Hora.AddHours(-1);
            else
                return Hora;
        }

        public static DateTime GetHoraUTCToLocal(DateTime Hora)
        {
            // la hora de la precipitacion viene en hora Utc
            // Hora.Kind = DateTimeKind.Utc;
            return Hora.ToLocalTime();
        }

        public static DateTime GetHoraLocalToUTC(DateTime Hora)
        {
            // la hora de la precipitacion viene en hora Utc
            // Hora.Kind = DateTimeKind.Utc;
            return Hora.ToUniversalTime();
        }    
    }//end class util
}
