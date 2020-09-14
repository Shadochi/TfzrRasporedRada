using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace RasporedRada.Servisi
{
    public static class PosaoServis
    {
        static string PutanjaFajla => ConfigurationManager.AppSettings["PosaoFajl"];

        public static IEnumerable<string> DajPosloveIzFajla()
        {
            IEnumerable<string> poslovi = Enumerable.Empty<string>();

            if (!string.IsNullOrWhiteSpace(PutanjaFajla))
            {
                try
                {
                    poslovi = System.IO.File.ReadAllLines(PutanjaFajla);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Putanja fajla za poslove je neispravna!");
                }
            }
            else
            {
                MessageBox.Show("Putanja fajla za poslove je prazna!");
            }

            return poslovi;
        }

    }
}
