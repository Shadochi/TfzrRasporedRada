using KlasePodataka;
using RasporedRada.Servisi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RasporedRada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string NazivBaze => ConfigurationManager.AppSettings["NazivBaze"];
        private string SqlInstanca => ConfigurationManager.AppSettings["SqlInstanca"];

        public MainWindow()
        {
            InitializeComponent();
            BinDataGrid();
        }
        private void BinDataGrid()
        {
            clsRasporedRadaDB rasporedRadaDB = new clsRasporedRadaDB(SqlInstanca, NazivBaze);
            DataGrid.ItemsSource = rasporedRadaDB.DajSveIzRasporeda().Tables["RasporedRada"].DefaultView;
        }

        private void cbxKlijenti_Ucitaj(object sender, RoutedEventArgs e)
        {
            List<string> klijenti = KlijentiServis.DajKlijenteIzFajla().ToList();

            if (klijenti != null && klijenti.Any())
            {
                foreach (string klijent in klijenti)
                {
                    if (!string.IsNullOrWhiteSpace(klijent))
                    {
                        cbxKlijenti.Items.Add(klijent);
                    }
                }
            }
            else
            {
                cbxKlijenti.Opacity = 0;
            }
        }

        private void cbxPosao_Ucitaj(object sender, RoutedEventArgs e)
        {
            List<string> poslovi = PosaoServis.DajPosloveIzFajla().ToList();

            if (poslovi != null && poslovi.Any())
            {
                foreach (string posao in poslovi)
                {
                    if (!string.IsNullOrWhiteSpace(posao))
                    {
                        cbxPosao.Items.Add(posao);
                    }
                }
            }
            else
            {
                cbxPosao.Opacity = 0;
            }
        }


        /*
         * Metoda za menjanje default texta na Date Picker-u iz "Select a date" u "Odaberi datum" pri učitavanju elementa
         * Preuzeto sa: https://social.msdn.microsoft.com/Forums/sqlserver/en-US/9eec87e0-4d12-430d-83fd-ce13dd96776b/datepicker-hide-quotselect-datequot-placeholder-or-change-it?forum=wpf
         * Metode Datum_Ucitan i FindVisualChild
         */
        private void Datum_Ucitan(object sender, RoutedEventArgs e) 
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                System.Windows.Controls.Primitives.DatePickerTextBox datePickerTextBox = FindVisualCild<System.Windows.Controls.Primitives.DatePickerTextBox>(datePicker);
                if (datePickerTextBox != null)
                {
                    ContentControl watermark = datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox) as ContentControl;
                    if (watermark != null)
                    {
                        watermark.Content = "Odaberi datum";
                    }
                }
            }
        }

        private T FindVisualCild<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                    T result = (child as T) ?? FindVisualCild<T>(child);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private void filter_Klik(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                MessageBox.Show("Uneti parametar za filter");
            }

            if (txtFilter.Text.Split(' ').Length != 2)
            {
                MessageBox.Show("Nije uneto ime i prezime, razdvojeno jednim razmakom");
            }
            else
            {

                clsRasporedRadaDB rasporedRadaDB = new clsRasporedRadaDB(SqlInstanca, NazivBaze);
                DataGrid.ItemsSource = rasporedRadaDB.DajRasporedPremaKorisniku(txtFilter.Text).Tables["RasporedRada"].DefaultView;
            }

            
        }

        private void resetuj_Klik(object sender, RoutedEventArgs e)
        {
            PonistiFilter();
            BinDataGrid();
        }

        private void PonistiFilter()
        {
            txtFilter.Text = string.Empty;
        }


        private void ponisti_Klik(object sender, RoutedEventArgs e)
        {
            ponistiFormu();
        }

        private void ponistiFormu()
        {
            txtIme.Text = string.Empty;
            txtPrezime.Text = string.Empty;
            txtSati.Text = "0";
            txtKlijent.Text = string.Empty;
            cbxPosao.SelectedIndex = -1;
            cbxKlijenti.SelectedIndex = -1;
            dpDatum.SelectedDate = null;
        }

        private void unesi_Klik(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                MessageBox.Show("Ime je prazno!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                MessageBox.Show("Prezime je prazno!");
            }

            if (string.IsNullOrWhiteSpace(txtKlijent.Text))
            {
                if (cbxKlijenti.SelectedItem == null)
                {
                    MessageBox.Show("Klijent nije odabran!");
                }
            }

            if (string.IsNullOrWhiteSpace(txtSati.Text))
            {
                bool rezultat = double.TryParse(txtSati.Text, out double iSati);
                if (rezultat == false)
                {
                    MessageBox.Show("Sati nisu uneti kao broj!");
                    return;
                }

                if (iSati <= 0)
                {
                    MessageBox.Show("Sati ne mogu biti manji ili jednaki 0");
                }
            }

            if (cbxPosao.SelectedItem == null)
            {
                MessageBox.Show("Posao nije odabran!");
            }

            clsRasporedRada objRasporedRada = new clsRasporedRada();

            string klijent = string.Empty;

            if (!string.IsNullOrWhiteSpace(txtKlijent.Text))
            {
                klijent = txtKlijent.Text;
            }
            else
            {
                klijent = cbxKlijenti.SelectedItem.ToString();
            }

            objRasporedRada.Ime = txtIme.Text;
            objRasporedRada.Prezime = txtPrezime.Text;
            objRasporedRada.Klijent = klijent;
            objRasporedRada.Posao = cbxPosao.SelectedItem.ToString();
            objRasporedRada.Sati = double.Parse(txtSati.Text);
            objRasporedRada.Datum = (DateTime)dpDatum.SelectedDate;
            clsRasporedRadaDB rasporedRadaDB = new clsRasporedRadaDB(SqlInstanca, NazivBaze);
            rasporedRadaDB.SnimiNoviUnosRada(objRasporedRada, out bool uspeh, out string greska);

            if (!uspeh)
            {
                MessageBox.Show(greska);
            }
            else
            {
                BinDataGrid();
                MessageBox.Show("Uspešno uneta stavka!");
                ponistiFormu();
            }
            
        }


        /*
         * Provera i ograničenje za textbox za sate preko Regex-a kako bi se samo decimalni brojevi mogli uneti
         */
        private void SamoBrojevi(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = DaLiJeTekstBroj(e.Text);

        }


        private static bool DaLiJeTekstBroj(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.-]+");
            return reg.IsMatch(str);

        }
    }
}
