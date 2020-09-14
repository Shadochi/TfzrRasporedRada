using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace RasporedRada.Servisi
{
    public static class KlijentiServis
    {
        static string PutanjaFajla => ConfigurationManager.AppSettings["KlijentiFajl"];

        public static IEnumerable<string> DajKlijenteIzFajla()
        {
            IEnumerable<string> klijenti = Enumerable.Empty<string>();

            if (!string.IsNullOrWhiteSpace(PutanjaFajla))
            {
                try
                {
                    klijenti = System.IO.File.ReadAllLines(PutanjaFajla);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Putanja fajla za klijente neispravna!");
                }
            }
            else
            {
                MessageBox.Show("Putanja fajla za klijente je prazna!");
            }

            return klijenti;
        }
    }
}
