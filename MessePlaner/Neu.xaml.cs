using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace MessePlaner
{
    /// <summary>
    /// Interaktionslogik für Neu.xaml
    /// </summary>
    public partial class Neu : Window
    { 
        public Neu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbExponat.Focus();
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            if (btnSpeichern.IsFocused == true)
            {
                if (MessageBox.Show("Möchten sie die Eingabe speichern?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    using (TestdbEntities contex = new TestdbEntities())
                    {
                        try
                        {
                            var neuesExponat = new Exponat()
                            {
                                Exponatname = tbExponat.Text,
                                Exponatnummer = Convert.ToInt32(tbNummer.Text),
                                Exponatzubehör = tbZubehör.Text,
                                Anzahl = Convert.ToInt32(tbAnzahl.Text),
                                Exponatversion = Convert.ToDecimal(tbVersion.Text)
                            };
                            contex.Exponat.Add(neuesExponat);

                            var neueMaße = new Maße()
                            {
                                Länge_cm = Convert.ToDecimal(tbLänge.Text),
                                Breite_cm = Convert.ToDecimal(tbBreite.Text),
                                Höhe_cm = Convert.ToDecimal(tbHöhe.Text),
                                Gewicht_kg = Convert.ToDecimal(tbGewicht.Text)
                            };
                            contex.Maße.Add(neueMaße);
                            
                            var neuZubehör = new Zubehör()
                            {
                                Zubehörname = tbZubehörname.Text,
                                Exponat_name = tbExponat.Text,
                                Länge_cm = Convert.ToDecimal(tbZLänge.Text),
                                Breite_cm = Convert.ToDecimal(tbZBreite.Text),
                                Höhe_cm = Convert.ToDecimal(tbZHöhe.Text),
                                Gewicht_kg = Convert.ToDecimal(tbZGewicht.Text),
                                Anzahl = Convert.ToInt32(tbZAnzahl.Text)
                            };
                            contex.Zubehör.Add(neuZubehör);

                            var neuVersand = new Versand()
                            {
                                Messe = tbMesse.Text,
                                Land = tbLand.Text,
                                Ort = tbOrt.Text,
                                Straße = tbStrasse.Text,
                                PLZ = tbPlz.Text,
                                Hausnummer = tbHausnummer.Text,
                                Datum = dpDatum.SelectedDate
                            };
                            contex.Versand.Add(neuVersand);

                            var neuRetoure = new Retoure()
                            {
                                Messe = tbRMesse.Text,
                                Land = tbRLand.Text,
                                Ort = tbROrt.Text,
                                Straße = tbRStrasse.Text,
                                PLZ = tbRPlz.Text,
                                Hausnummer = tbRHausnummer.Text,
                                Datum = dpRDatum.SelectedDate
                            };
                            contex.Retoure.Add(neuRetoure);

                            contex.SaveChanges();

                            var textboxlist = new TextBox[] { tbAnzahl, tbBreite, tbExponat, tbExponatname, tbGewicht, tbHöhe, tbLänge, tbNummer, tbVersion, 
                                                              tbZAnzahl, tbZBreite, tbZGewicht, tbZHöhe, tbZLänge, tbZubehör, tbMesse, tbLand, tbOrt, tbStrasse, tbPlz, tbHausnummer,
                                                              tbRMesse, tbRLand, tbROrt, tbRStrasse, tbRPlz, tbRHausnummer };

                            foreach (var tc in textboxlist)
                            {
                                tc.Clear();
                            }
                            return;
                        }
                        catch (Exception ex)
                        {
                            
                            MessageBox.Show(ex.Message);
                            
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnSchliessen_Click(object sender, RoutedEventArgs e)
        {
            var textboxlist = new TextBox[] { tbExponat, tbNummer, tbAnzahl, tbVersion, tbLänge, tbBreite, tbHöhe, tbGewicht, tbExponatname, tbZubehörname, tbZAnzahl, tbZBreite, tbZGewicht, tbZHöhe, tbZLänge, tbZubehör };

            foreach (var tb in textboxlist)
            {
                if (tb.Text != string.Empty)
                {
                    if (MessageBox.Show("Wenn sie das Fenster schließen, werden die eingegebenen Daten gelöscht", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Close();
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            Close();
        }

        private void tbExponat_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbExponatname.Text = tbExponat.Text;
            tbZubehörname.Text = tbZubehör.Text;
            tbRMesse.Text = tbMesse.Text;
            tbRLand.Text = tbLand.Text;
            tbROrt.Text = tbOrt.Text;
            tbRStrasse.Text = tbStrasse.Text;
            tbRPlz.Text = tbPlz.Text;
            tbRHausnummer.Text = tbHausnummer.Text;

            var textboxlist = new TextBox[] { tbAnzahl, tbBreite, tbExponat, tbExponatname, tbGewicht, tbHöhe, tbLänge, tbNummer, tbVersion,
                                              tbZAnzahl, tbZBreite, tbZGewicht, tbZHöhe, tbZLänge, tbZubehör, tbMesse, tbLand, tbOrt, tbStrasse, tbPlz, tbHausnummer,
                                              tbRMesse, tbRLand, tbROrt, tbRStrasse, tbRPlz, tbRHausnummer };

            foreach (var tc in textboxlist)
            {
                if(String.IsNullOrEmpty(tc.Text))
                {
                    tc.Background = new SolidColorBrush(Color.FromArgb(23, 255, 0, 0));
                    btnSpeichern.IsEnabled = false;
                }
                else
                {
                    tc.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    btnSpeichern.IsEnabled = true;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            using (TestdbEntities contex = new TestdbEntities())
            {
                var query = from exponat in contex.Exponat
                            select new { exponat.Exponat_ID, exponat.Exponatname, exponat.Exponatnummer, exponat.Anzahl, exponat.Exponatversion, exponat.Exponatzubehör };
                main.dgTest.ItemsSource = query.ToList();
                main.dgTest.Items.Refresh();
                main.Show();
            }
        }
    }
}
