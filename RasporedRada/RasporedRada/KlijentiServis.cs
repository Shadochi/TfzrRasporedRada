using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace RasporedRada
{
    public static class KlijentiServis
    {
        static readonly string PutanjaFajla = ConfigurationManager.AppSettings["KlijentiFajl"];

        public static IEnumerable<string> DajKlijenteIzFajla()
        {
            IEnumerable<string> klijenti = Enumerable.Empty;

            if (!string.IsNullOrWhiteSpace(PutanjaFajla))
            {
                klijenti = System.IO.File.ReadAllLines(PutanjaFajla);
            }

            return klijenti;
        }
    }
}
