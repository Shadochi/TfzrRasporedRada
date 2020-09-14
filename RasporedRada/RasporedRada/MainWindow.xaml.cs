using RasporedRada.Servisi;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RasporedRada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
