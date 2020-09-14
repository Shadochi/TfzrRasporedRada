using System.Data.SqlClient;

namespace SqlDBUtils
{
    public class clsSqlKonekcija
    {
        /* ODGOVORNOST: Konekcija na celinu baze podataka, SQL server tipa  */

        #region Atributi
        private SqlConnection pKonekcija;
        //
        private string pPutanjaSQLBaze;
        private string pNazivBaze;
        private string pNazivSQL_DBMSinstance;
        private string pStringKonekcije;

        #endregion

        #region Konstruktor
        public clsSqlKonekcija(string nazivSQL_DBMSInstance, string putanjaSqlBaze, string NazivBaze)
        {
            // preuzimanje vrednosti u privatne atribute
            pPutanjaSQLBaze = putanjaSqlBaze;
            pNazivBaze = NazivBaze;
            pNazivSQL_DBMSinstance = nazivSQL_DBMSInstance;
            // pripremanje stringa konekcije, dodato 22.3.2020.
            pStringKonekcije = "";
            pStringKonekcije = FormirajStringKonekcije();
        }

        // preklapajuci konstruktor, kada dobijemo spolja string konekcije
        public clsSqlKonekcija(string noviStringKonekcije)
        {
            pStringKonekcije = noviStringKonekcije; // u ovom slucaju se ne poziva metoda FormirajStringKonekcije
        }

        // preklapajuci konstruktor, bez stringa konekcije
        public clsSqlKonekcija()
        {

        }

        #endregion

        #region Privatne metode
        private string FormirajStringKonekcije() // promenjen naziv
        {
            string mStringKonekcije = "";
            if (pPutanjaSQLBaze.Length.Equals(0) || pPutanjaSQLBaze == null)
            {
                mStringKonekcije = "Data Source=" + pNazivSQL_DBMSinstance + " ;Initial Catalog=" + pNazivBaze + ";Integrated Security=True";
            }
            else
            {
                mStringKonekcije = "Data Source=.\\" + pNazivSQL_DBMSinstance + ";AttachDbFilename=" + pPutanjaSQLBaze + "\\" + pNazivBaze + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
            }
            return mStringKonekcije;
        }
        #endregion

        #region Javne metode

        public string StringKonekcije
        {
            get { return pStringKonekcije; }
            set { pStringKonekcije = value; }
        }


        public bool OtvoriKonekciju()
        {
            bool uspeh;
            pKonekcija = new SqlConnection();
            pKonekcija.ConnectionString = pStringKonekcije; // 22.3.2020. s obzirom da se na konstruktoru formira i smesta string konekcije u pStringKonekcije, onda se ovde samo otvara
            // ovo je bolje resenje nego da se ovde pozove i kreiranje stringa konekcije, s obzirom na SOLID princip Single Responsibility i a sto jezgrovitije radimo posao koji je dat (da se ne radi ono sto nije u nazivu)

            try
            {
                pKonekcija.Open();
                uspeh = true;
            }
            catch
            {
                uspeh = false;
            }
            return uspeh;
        }

        public SqlConnection DajKonekciju()
        {
            return pKonekcija;
        }

        public string DajStringKonekcije() // dodato 22.3.2020.
        {
            return pStringKonekcije;
        }

        public void ZatvoriKonekciju()
        {
            pPutanjaSQLBaze = "";
            pKonekcija.Close();
            pKonekcija.Dispose();
        }

        #endregion

    }
}
