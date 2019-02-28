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
            if (ts.TotalHours < 1)
            {
                return "Il y a " + (int)ts.TotalMinutes + " minutes";
            }
            else if (ts.TotalDays < 1)
            {
                return "Il y a " + (int)ts.TotalHours + " heures";
            }
            else
            {
                return "Il y a " + (int)ts.TotalDays + " jours";
            }

        }
    }
}
