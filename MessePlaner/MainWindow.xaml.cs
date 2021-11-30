using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MessePlaner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (TestdbEntities contex = new TestdbEntities())
            {
                var query = from exponat in contex.Exponat
                            select new { exponat.Exponat_ID, exponat.Exponatname, exponat.Exponatnummer, exponat.Anzahl, exponat.Exponatversion, exponat.Exponatzubehör };
                dgTest.ItemsSource = query.ToList();
            }
        }

        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Möchten sie Programm wirklich schließen?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            Neu fensterneu = new Neu();
            try
            {
                fensterneu.ShowDialog();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // über Combobox Datagrid Ansicht wechseln
            using (TestdbEntities contex = new TestdbEntities())
            {
                contex.Database.Connection.Open();
                try
                {
                    if (cbExponat.IsSelected)
                    {
                        var query = from exponat in contex.Exponat
                                    select new { exponat.Exponat_ID, exponat.Exponatname, exponat.Exponatnummer, exponat.Exponatversion, exponat.Exponatzubehör };
                        dgTest.ItemsSource = query.ToList();
                        dgTest.Items.Refresh();
                    }
                    else if (cbMaße.IsSelected)
                    {
                        var query = from maße in contex.Maße
                                    select new { maße.Maße_ID, maße.Länge_cm, maße.Breite_cm, maße.Höhe_cm, maße.Gewicht_kg };
                        dgTest.ItemsSource = query.ToList();
                        dgTest.Items.Refresh();
                    }
                    else if (cbZubehör.IsSelected)
                    {
                        var query = from zubehör in contex.Zubehör
                                    select new { zubehör.Zubehör_ID, zubehör.Zubehörname, zubehör.Exponat_name, zubehör.Länge_cm, zubehör.Breite_cm, zubehör.Höhe_cm, zubehör.Gewicht_kg };
                        dgTest.ItemsSource = query.ToList();
                        dgTest.Items.Refresh();
                    }
                    else if (cbRetoure.IsSelected)
                    {
                        var query = from retoure in contex.Retoure
                                    select new { retoure.Messe_ID, retoure.Messe, retoure.Land, retoure.Ort, retoure.Straße, retoure.Hausnummer, retoure.PLZ, retoure.Datum };
                        dgTest.ItemsSource = query.ToList();
                        dgTest.Items.Refresh();
                    }
                    else if (cbVersand.IsSelected)
                    {
                        var query = from versand in contex.Versand
                                    select new { versand.Messe_ID, versand.Messe, versand.Land, versand.Ort, versand.Straße, versand.Hausnummer, versand.PLZ, versand.Datum };
                        dgTest.ItemsSource = query.ToList();
                        dgTest.Items.Refresh();
                    }
                    else if (cbAlle.IsSelected)
                    {
                        var query = from exponat in contex.Exponat
                                    join maße in contex.Maße
                                    on exponat.Exponat_ID equals maße.Exponat_ID
                                    select new { exponat.Exponatname, exponat.Exponatnummer, exponat.Anzahl, exponat.Exponatzubehör, 
                                                 maße.Länge_cm, maße.Breite_cm, maße.Höhe_cm, maße.Gewicht_kg };
                        dgTest.ItemsSource = query.ToList();
                        dgTest.Items.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                contex.Database.Connection.Close();
            }
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(btnNeu.IsEnabled == false)
                {
                    btnNeu.IsEnabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //Suchabfrage über Textbox
                using (TestdbEntities contex = new TestdbEntities())
                {
                    if (cbExponat.IsSelected == true)
                    {
                        var query = from exponat in contex.Exponat
                                    select new { exponat.Exponat_ID, exponat.Exponatname, exponat.Exponatnummer, exponat.Anzahl, exponat.Exponatversion, exponat.Exponatzubehör };
                       
                        query = query.Where(exponat => exponat.Exponatname.Contains(tbSuche.Text));
                        dgTest.ItemsSource = query.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
