using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            tbCambiaTexto.Text = lblPrincipal.Text;
        }

        private void cbFuentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cbFuentes.SelectedItem as ComboBoxItem;
            if (item != null)
            {
                string fuente = item.Content.ToString();
                lblPrincipal.FontFamily = new System.Windows.Media.FontFamily(fuente);
            }
        }

        private void tbLetra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validar que el texto ingresado sea un número
            if (!int.TryParse(e.Text, out int value))
            {
                e.Handled = true; // Ignorar entrada no numérica
                return;
            }
        }

        private void tbLetra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(tbLetra.Text, out int size))
            {
                if (size < 1 || size > 72)
                {
                    size = 12; // Si el valor está fuera de rango, establece el tamaño a 12 (valor predeterminado)
                }

                // Cambia el tamaño de la fuente del label
                lblPrincipal.FontSize = size;
            }
            else
            {
                lblPrincipal.FontSize = 12; // Si el valor no es numérico, establece el tamaño a 12 (valor predeterminado)
            }
        }

        private void btBold_Click(object sender, RoutedEventArgs e)
        {
            if (lblPrincipal.FontWeight != FontWeights.Bold)
            {
                lblPrincipal.FontWeight = FontWeights.Bold;
            }
            else
            {
                lblPrincipal.FontWeight = FontWeights.Normal;
            }
        }

        private void btCursiva_Click(object sender, RoutedEventArgs e)
        {
            if (lblPrincipal.FontStyle != FontStyles.Italic)
            {
                lblPrincipal.FontStyle = FontStyles.Italic;
            }
            else
            {
                lblPrincipal.FontStyle = FontStyles.Normal;
            }
        }

        private void btSubr_Click(object sender, RoutedEventArgs e)
        {
            if (lblPrincipal.TextDecorations != TextDecorations.Underline)
            {
                lblPrincipal.TextDecorations = TextDecorations.Underline;
            }
            else
            {
                lblPrincipal.TextDecorations = null;
            }
        }

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                System.Windows.Media.Color C = cp.SelectedColor.Value;

                SolidColorBrush solidColorBrush = new SolidColorBrush(C);

                lblPrincipal.Foreground = solidColorBrush;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ventanaPrincipal = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (ventanaPrincipal.labelModificar!=null)
            {
                ventanaPrincipal.labelModificar.Content = lblPrincipal.Text;
                ventanaPrincipal.labelModificar.FontFamily = lblPrincipal.FontFamily;
                ventanaPrincipal.labelModificar.FontSize = lblPrincipal.FontSize;
                ventanaPrincipal.labelModificar.FontStyle = lblPrincipal.FontStyle;
                ventanaPrincipal.labelModificar.FontStyle = lblPrincipal.FontStyle;
                ventanaPrincipal.labelModificar.FontWeight = lblPrincipal.FontWeight;
                ventanaPrincipal.labelModificar.Foreground = lblPrincipal.Foreground;
            }
            
            this.Close();
            
        }

        private void tbCambiaTexto_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblPrincipal.Text = tbCambiaTexto.Text;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var ventanaPrincipal = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (ventanaPrincipal.labelModificar != null)
            {
                ventanaPrincipal.labelModificar.ReleaseMouseCapture();
            }
        }

    }
}
