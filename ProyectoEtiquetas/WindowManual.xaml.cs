using System;
using System.Collections.Generic;
using System.IO;
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
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.Wpf;

namespace ProyectoEtiquetas
{
    /// <summary>
    /// Lógica de interacción para WindowManual.xaml
    /// </summary>
    public partial class WindowManual : Window
    {
        public WindowManual()
        {
            InitializeComponent();
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"manualetiquetas.pdf");
            if(!File.Exists(filePath))
            {
                filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"TagFarma\manualetiquetas.pdf");
            }

            if (File.Exists(filePath))
            {
                var doc = Patagames.Pdf.Net.PdfDocument.Load(filePath);
                pdfViewer.Document = doc;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
