using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace eRestoran
{
    public partial class PrezentacijaJela : PhoneApplicationPage
    {
        public PrezentacijaJela()
        {
            InitializeComponent();
            using (eRestoranLokalBazaContext db = new eRestoranLokalBazaContext(eRestoranLokalBazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
                try
                {
                    Table<Jela> jela = db.GetTable<Jela>();
                    var jelaQuery = from j in jela.ToList() select j;
                    foreach (var jelo in jelaQuery)
                    {
                       PivotItem p = new PivotItem();
                       JeloKontrola jeloKontrola = new JeloKontrola();
                        jeloKontrola.ImeText.Text =  jelo.Cijena + " KM";
                        jeloKontrola.OpisText.Text =  jelo.Opis;
                        if (jelo.Slika.ToArray() != null && jelo.Slika.ToArray() is Byte[])
                        {
                            MemoryStream stream = new MemoryStream(jelo.Slika.ToArray());
                            BitmapImage image = new BitmapImage();
                            image.SetSource(stream);
                            jeloKontrola.slikaKontrola.Source = image;
                        }
                        p.Header = jelo.Ime;
                        p.Content = jeloKontrola;
                        mojPivot.Items.Add(p);
                    }

                }
                catch (Exception et)
                {

                }
            }
        }

        private void Pivot_Loaded_1(object sender, RoutedEventArgs e)
        {
 
        }
    }
}