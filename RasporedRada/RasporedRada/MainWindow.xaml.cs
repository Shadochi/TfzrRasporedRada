using KlasePodataka;
using RasporedRada.Servisi;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ponisti_Klik(object sender, RoutedEventArgs e)
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

        }
    }
}
