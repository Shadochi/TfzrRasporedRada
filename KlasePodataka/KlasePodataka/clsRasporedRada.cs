using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePodataka
{
    public class clsRasporedRada
    {
        // NAMENA: Klasa ciji objekat koja odgovara jednom zapisu iz baze podataka, ima samo set-get metode

        // atributi
        private string _KorisnikIme;
        private string _KorisnikAdresa;
        private string _Proizvod;
        private int _Kolicina;
        private int _Cena;
        private DateTime _DatumIsporuke;

        // konstruktor
        public clsNarucivanje()
        {
            // inicijalizacija atributa
            _KorisnikIme = "";
            _KorisnikAdresa = "";
            _Proizvod = "";
            _Kolicina = 0;
            _Cena = 0;
            _DatumIsporuke = DateTime.Now;

        }

        // public 
        public string KorisnikIme
        {
            get { return _KorisnikIme; }
            set { _KorisnikIme = value; }
        }

        public string KorisnikAdresa
        {
            get { return _KorisnikAdresa; }
            set { _KorisnikAdresa = value; }
        }

        public string Proizvod
        {
            get { return _Proizvod; }
            set { _Proizvod = value; }
        }

        public int Kolicina
        {
            get { return _Kolicina; }
            set { _Kolicina = value; }
        }

        public int Cena
        {
            get { return _Cena; }
            set { _Cena = value; }
        }

        public DateTime DatumIsporuke
        {
            get { return _DatumIsporuke; }
            set { _DatumIsporuke = value; }
        }

    }
}
