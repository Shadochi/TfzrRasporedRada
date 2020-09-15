using System;

namespace KlasePodataka
{
    public class clsRasporedRada
    {
        // NAMENA: Klasa ciji objekat koja odgovara jednom zapisu iz baze podataka, ima samo set-get metode

        // atributi
        private string _Ime;
        private string _Prezime;
        private string _Klijent;
        private string _Posao;
        private int _Sati;
        private DateTime  _Datum;

        // konstruktor
        public clsRasporedRada()
        {
            // inicijalizacija atributa
            _Ime = string.Empty;
            _Prezime = string.Empty;
            _Klijent = string.Empty;
            _Posao = string.Empty;
            _Sati = 0;
            _Datum = DateTime.Now;

        }

        // public 
        public string Ime
        {
            get { return _Ime; }
            set { _Ime = value; }
        }

        public string Prezime
        {
            get { return _Prezime; }
            set { _Prezime = value; }
        }

        public string Klijent
        {
            get { return _Klijent; }
            set { _Klijent = value; }
        }

        public string Posao
        {
            get { return _Posao; }
            set { _Posao = value; }
        }

        public int Sati
        {
            get { return _Sati; }
            set { _Sati = value; }
        }

        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }

    }
}
