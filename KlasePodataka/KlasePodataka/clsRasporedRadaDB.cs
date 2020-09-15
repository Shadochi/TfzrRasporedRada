using SqlDBUtils;
using System;
using System.Data;
namespace KlasePodataka
{
    public class clsRasporedRadaDB
    {
        //atributi
        private clsSqlKonekcija objSqlKonekcija;
        private clsSqlTabela objSqlTabela;
        private DataSet dsRasporedRada;

        //konstruktor
        public clsRasporedRadaDB(string MSSQLInstanca, string NazivBazePodataka)
        {
            objSqlKonekcija = new clsSqlKonekcija(MSSQLInstanca, "", NazivBazePodataka);
            objSqlKonekcija.OtvoriKonekciju();
            objSqlTabela = new clsSqlTabela(objSqlKonekcija, "RasporedRada");
            dsRasporedRada = new DataSet();
        }

        public DataSet DajSveIzRasporeda()
        {
            dsRasporedRada = objSqlTabela.DajPodatke("SELECT * FROM RasporedRada");
            return dsRasporedRada;
        }

        public DataSet DajRasporedPremaKorisniku(string ImeIPrezime)
        {
            //Razdvojiti ime i prezime u niz stringova na osnovu razmaka
            string[] imeIPrezime = ImeIPrezime.Split(' ');
            dsRasporedRada = objSqlTabela.DajPodatke($"SELECT * FROM RasporedRada WHERE Ime = '{imeIPrezime[0]}' AND Prezime = '{imeIPrezime[1]}'");
            return dsRasporedRada;
        }

        public void SnimiNoviUnosRada(clsRasporedRada objRasporedRada, out bool uspeh, out string tekstGreske)
        {
            string strDatum = $"{objRasporedRada.Datum.Month}/{objRasporedRada.Datum.Day}/{objRasporedRada.Datum.Year}";

            string sqlInsertUpit = $"INSERT INTO RasporedRada VALUES('{objRasporedRada.Ime}','{objRasporedRada.Prezime}','{objRasporedRada.Klijent}', '{objRasporedRada.Sati}', '{strDatum}', '{objRasporedRada.Posao}')";
            
            try
            {
                uspeh = objSqlTabela.IzvrsiAzuriranje(sqlInsertUpit);
                tekstGreske = string.Empty;
            }
            catch (Exception greska)
            {
                uspeh = false;
                tekstGreske = greska.Message;
            }
        }
    }
}
