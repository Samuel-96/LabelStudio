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

namespace ProyectoEtiquetas
{
    /// <summary>
    /// Lógica de interacción para WindowMargenes.xaml
    /// </summary>
    public partial class WindowMargenes : Window
    {
        public int margenIzquierdo = 0, margenSuperior = 0, margenDerecho = 0, margenInferior = 0;
        public WindowMargenes()
        {
            InitializeComponent();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            margenIzquierdo = int.Parse(tbMargenIzquierdo.Text);
            margenSuperior = int.Parse(tbMargenSuperior.Text);
            margenDerecho = int.Parse(tbMargenDerecho.Text);
            margenInferior = int.Parse(tbMargenInferior.Text);
            Close();
        }
    }
}
