using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Utils
{
    class TempsUtils
    {
        public static string DatePublicationToTemps(DateTime datePublication)
        {
            TimeSpan ts = DateTime.Now.Subtract(datePublication);
            if (ts.TotalMinutes < 1)
            {
                return "Il y a moins d'une minute";
            }
            else if (ts.TotalHours < 1)
            {
                return "Il y a " + (int)ts.TotalMinutes + " minute(s)";
            }
            else if (ts.TotalDays < 1)
            {
                return "Il y a " + (int)ts.TotalHours + " heure(s)";
            }
            else
            {
                return "Il y a " + (int)ts.TotalDays + " jour(s)";
            }

        }
    }
}
