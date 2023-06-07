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
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using MySqlConnector;
using System.Reflection;

namespace ProyectoEtiquetas
{
    /// <summary>
    /// Lógica de interacción para TamCanvas.xaml
    /// </summary>
    public partial class TamCanvas : Window
    {
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public int NumCopias { get; set; }
        public string rutaPlantillas;
        public string rutaPlantilla;
        public bool PlantillaSeleccionada { get; set; }
        public string MaterialAcondicionamiento { get; set; }
        public string Tam { get; set; }
        public bool matAconSeleccionado = false;
        public bool tamImpresionSeleccionado = false;

        public TamCanvas()
        {
            InitializeComponent();

            //OBTENEMOS LAS PLANTILLAS DESDE LA RUTA DEL CONFIG.INI Y LAS MOSTRAMOS EN EL LISTBOX CON EL NOMBRE DE LA PLANTILLA SIN LA EXTENSION
            try
            {
                rutaPlantillas = System.IO.Path.Combine(Environment.CurrentDirectory, @"config.ini");
                if(!File.Exists(rutaPlantillas))
                {
                    rutaPlantillas = System.IO.Path.Combine(Environment.CurrentDirectory, @"TagFarma\config.ini");
                }
                rutaPlantillas = MainWindow.ObtenerRuta(rutaPlantillas);
                //rutaPlantillas = MainWindow.ObtenerRuta(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\config.ini"));
                string[] archivosPlantillas = Directory.GetFiles(rutaPlantillas, "*.xml");
                if (archivosPlantillas.Length > 0)
                {
                    foreach (string archivo in archivosPlantillas)
                    {
                        lstPlantillas.Items.Add(System.IO.Path.GetFileNameWithoutExtension(archivo));
                    }
                }
                else
                {
                    lstPlantillas.Items.Add("No se han encontrado plantillas en " + rutaPlantillas);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("No se ha podido obtener la ruta a config.ini");
            }

        }


        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
                int height = 0;
                if ( (int.TryParse(tbAnchura.Text, out int width) && int.TryParse(tbAltura.Text, out height) || (tamImpresionSeleccionado == true) ))
                {
                    int dpi = 96;
                    CanvasWidth = (int)(width * dpi / 25.4);
                    CanvasHeight = (int)(height * dpi / 25.4);

                    if (int.TryParse(tbNumCopias.Text, out int numCopias))
                    {
                        NumCopias = numCopias;
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado un número de copias");
                    }  
                }

                else
                {
                    MessageBox.Show("Tamaño introducido no válido.");
                }
            
               
        }

        private void lstPlantillas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

                if (lstPlantillas.SelectedItem != null)
                {
                    if (int.TryParse(tbNumCopias.Text, out int numCopias))
                    {
                        NumCopias = numCopias;
                        string? plantilla = lstPlantillas.SelectedItem.ToString();
                        rutaPlantilla = System.IO.Path.Combine(rutaPlantillas, plantilla + ".xml");
                        PlantillaSeleccionada = true;
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado un número de copias");
                    }
                }
        }

        private void cbMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbMaterialAcon = sender as ComboBox;
            if (cbMaterialAcon != null)
            {
                var materialSeleccionado = cbMaterialAcon.SelectedItem as string;
                if (materialSeleccionado != null)
                {
                    MaterialAcondicionamiento = materialSeleccionado;
                    matAconSeleccionado = true;
                }
            }
        }

        private void cbTam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbTam = sender as ComboBox;

                ComboBoxItem ComboItem = (ComboBoxItem)cbTam.SelectedItem;
                string tam = ComboItem.Content.ToString();
                if (tam != " ")
                {
                    Tam = tam;
                    tamImpresionSeleccionado = true;
                }
                else
                {
                    tamImpresionSeleccionado = false;
                }
        }

        private void tb_NumCopias(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
            
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
