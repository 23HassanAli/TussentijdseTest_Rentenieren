using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TussentijdseTest_Rentenieren
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double hetBedrag;
        int investeringAantalJaren;
        double renteVoet = 2;
        double totaal;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInvesteringBekenenen_Click(object sender, RoutedEventArgs e)
        {
            InvesteringBerekenen(CheckInputTxtBox());
        }
        private void InvesteringBerekenen(bool checkInputTxtBox)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (checkInputTxtBox == true)
            {
                btnInvesteringBekenenen.Background = Brushes.Gray;
                //btnInvesteringBekenenen.IsEnabled = true;
                tbxInputBedrag.Clear();
                tbxInputAantalJaren.Clear();
                if (ChkbxStandaardRente.IsChecked == true)
                {
                    renteVoet += 2;
                    hetBedrag -= 1000;

                }
                if (tbxInputRente.Text != "" && double.TryParse(tbxInputRente.Text, out double rente))
                    renteVoet = rente;
                double interest = renteVoet / 100;
                stringBuilder.AppendLine($"Startbedrag: {hetBedrag}, jaarlijkse rente = {renteVoet}, Aantaljaren {investeringAantalJaren}");
                for (int i = 1; i < investeringAantalJaren + 1; i++)
                {
                    totaal = hetBedrag * Math.Pow((1 + interest / 1),
                                             (1 * i));
                    if (i == 1)
                    {
                        stringBuilder.AppendLine($"Waarde na {i} jaar {totaal:N}");

                    }
                    else
                        stringBuilder.AppendLine($"Waarde na {i} jaren {totaal:N}");

                }
                tbxOutput.Text = stringBuilder.ToString();



            }


        }
        private bool CheckInputTxtBox()
        {
            string stringBedrag = tbxInputBedrag.Text;
            string stringjaren = tbxInputAantalJaren.Text;
            if (string.IsNullOrWhiteSpace(stringBedrag))
            {
                btnInvesteringBekenenen.Background = Brushes.Red;
                return false;
            }
            if (string.IsNullOrWhiteSpace(stringjaren))
            {
                btnInvesteringBekenenen.Background = Brushes.Red;
                return false;
            }
            hetBedrag = double.Parse(stringBedrag);
            bool isNummeriek = double.TryParse(stringBedrag, out hetBedrag);


            investeringAantalJaren = int.Parse(stringjaren);
            bool isNummeriek2 = int.TryParse(stringjaren, out investeringAantalJaren);


            if (isNummeriek == false || isNummeriek2 == false)
            {
                btnInvesteringBekenenen.Background = Brushes.Red;
                return false;
            }
            if (hetBedrag <= 0 || investeringAantalJaren < 1)
            {
                btnInvesteringBekenenen.Background = Brushes.Red;
                return false;
            }
            return true;


        }


        private void btnAanmelden_Click(object sender, RoutedEventArgs e)
        {
            string naamKlant = tbxNaamKlant.Text;
            if (string.IsNullOrWhiteSpace(naamKlant))
            {
                tblIngelogd.Text = "Je bent niet ingelogd";
                tblIngelogd.Background = Brushes.LightSalmon;
            }
            else
            {
                tblIngelogd.Text = "Je bent ingelogd als " + naamKlant;
                tblIngelogd.Background = Brushes.LightGreen;
                btnAfmelden.Visibility = Visibility.Visible;
                tbxNaamKlant.IsEnabled = false;
            }
            btnInvesteringBekenenen.IsEnabled = true;


        }

        private void btnAfmelden_Click(object sender, RoutedEventArgs e)
        {
            tblIngelogd.Text = "Je bent niet ingelogd.";
            tblIngelogd.Background = Brushes.LightSalmon;
            btnAfmelden.Visibility = Visibility.Hidden;
            btnInvesteringBekenenen.IsEnabled = false;
            tbxNaamKlant.IsEnabled = true;
            btnInvesteringBekenenen.IsEnabled = true;
            ChkbxStandaardRente.IsChecked = false;
        }
    }
}
