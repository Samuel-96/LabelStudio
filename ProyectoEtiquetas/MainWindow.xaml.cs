using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MySqlConnector;
using Xceed.Wpf.Toolkit;
using static Org.BouncyCastle.Math.EC.ECCurve;
using PdfiumViewer;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.Wpf;
using Microsoft.VisualBasic;
using System.Printing;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Reflection.PortableExecutable;

namespace ProyectoEtiquetas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int canvasWidth = 0; //anchura elegida por el usuario en la primera ventana
        private int canvasHeight = 0; //altura elegida por el usuario en la primera ventana
        private int numCopias = 1; //numero de paginas elegido por el usuario en la primera ventana
        private string tamImpresion; //tamaño de la impresion elegido por el usuario en la primera ventana
        public int margenIzquierdo = 0, margenSuperior = 0, margenDerecho = 0, margenInferior = 0; //los margenes de 
        public bool arrastrable = true;
        public bool estaArrastrando = false;
        private Label labelArrastrando = null; //declaramos una variable label que obtendrá la label que está siendo arrastrada para solucionar el problema del movimiento en los labels del canvas
        private object objetoArrastrado;
        public Label labelModificar;
        private String rutaArchivo;
        private Label labelSeleccionada = null;
        private List<object> items = null;
        private List<object> items2 = null;
        private List<object> items3 = null;
        public static string rutaPlantillas;

        private string rutaPlantilla;
        private int idArgumento;
        private int contComponentes = 1;

        public MainWindow()
        {
            try
            {
                //de todo el programa primero se ejecuta la linea de InitializeComponent, luego la de ObtenerConexion y luego la de InitilizeComponent en el TamCanvas
                InitializeComponent();

                var ventanaTamCanvas = new TamCanvas();
                if (ventanaTamCanvas.ShowDialog() == true)
                {
                    canvasWidth = ventanaTamCanvas.CanvasWidth;
                    canvasHeight = ventanaTamCanvas.CanvasHeight;
                    numCopias = ventanaTamCanvas.NumCopias;
                    if(ventanaTamCanvas.Tam != null || ventanaTamCanvas.Tam != " ")
                    {
                        tamImpresion = ventanaTamCanvas.Tam;
                    }
                    canvas.Width = canvasWidth;
                    canvas.Height = canvasHeight;
                    PreviewKeyDown += OnPreviewKeyDown;

                    if (ventanaTamCanvas.PlantillaSeleccionada == true)
                    {
                        rutaPlantilla = ventanaTamCanvas.rutaPlantilla;
                    }

                }

                else
                {
                    //Si el usuario cierra la ventana de seleccion de tamaño se cierra la aplicacion
                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error -> " + ex.Message);
            }

        }

        //cuando se hace click en el boton izq del raton 
        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (arrastrable && !string.IsNullOrEmpty((sender as ListBoxItem).Content?.ToString()))
            {
                estaArrastrando = true;
                objetoArrastrado = sender;
            }

            else
                estaArrastrando = false;
        }

        //metodo para mover los items del listbox al canvas
        private void ListBoxItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (estaArrastrando && sender == objetoArrastrado)
            {

                // Crear objeto DataObject que almacena el item de la listbox que esta siendo arrastrado
                DataObject data = new DataObject(typeof(ListBoxItem), sender);

                // Iniciar proceso de arrastre
                DragDrop.DoDragDrop(sender as DependencyObject, data, DragDropEffects.Move);

                // Restablecemos el objeto que estamos arrastrando
                estaArrastrando = false;
                objetoArrastrado = null;
            }
        }

        //se encarga de cambiar el estado de estaArrastrando a true cuando se hace click en el label del canvas
        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            arrastrable = true;
            labelArrastrando = sender as Label; //obtenemos el label que ha sido seleccionado
        }

        //se encarga de cambiar el estado de estaArrastrando a false cuando se suelta el click
        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //estaArrastrando = false;
            labelArrastrando = null;
        }

        //se encarga de poder mover los label por el canvas
        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            //añadimos otra condicion, la de mouseButttonState para evitar que el programa detecte que el click del raton esta presionado
            if (arrastrable && e.LeftButton == MouseButtonState.Pressed && labelArrastrando == sender) //añadimos otra condicion para que solo se pueda mover el label que esta siendo arrastrado
            {
                System.Windows.Point mousePosition = e.GetPosition(canvas);

                // Definir límites del movimiento
                double leftLimit = 0;
                double topLimit = 0;
                double rightLimit = canvas.ActualWidth - (sender as FrameworkElement).ActualWidth;
                double bottomLimit = canvas.ActualHeight - (sender as FrameworkElement).ActualHeight;

                // Definir nueva posición del objeto dentro de los límites del canvas para evitar que se salga del mismo
                double newLeft = Math.Max(leftLimit, Math.Min(rightLimit, mousePosition.X - ((sender as FrameworkElement).ActualWidth / 2)));
                double newTop = Math.Max(topLimit, Math.Min(bottomLimit, mousePosition.Y - ((sender as FrameworkElement).ActualHeight / 2)));

                Canvas.SetLeft(sender as UIElement, newLeft);
                Canvas.SetTop(sender as UIElement, newTop);

                // Establecer el índice Z del elemento arrastrado para asegurarse de que está por encima del resto y evitar colisiones
                int maxZIndex = canvas.Children.OfType<UIElement>().Max(e => Canvas.GetZIndex(e));
                Canvas.SetZIndex(sender as UIElement, maxZIndex + 1);
            }
        }

        //evento que actua cuando se arrastra algo al canvas
        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            // Recuperar el elemento que se está arrastrando
            ListBoxItem draggedItem = (ListBoxItem)e.Data.GetData(typeof(ListBoxItem));

            // Crear un nuevo objeto que represente el elemento en el canvas
            Label label = new Label();
            label.MouseDown += Label_MouseDown; //agregamos evento para cuando se hace click y se mantiene en el label
            label.MouseMove += Label_MouseMove; //agregamos evento para que se pueda mover el label por el canvas
            label.MouseUp += Label_MouseUp;     //agregamos evento para cuando se suelta el click y se ha terminado de arrastrar el elemento
            label.MouseDoubleClick += lbl_DoubleClick; //agregamos evento para cuando se hace click en el label y se abre la ventana de edicion
            label.PreviewMouseLeftButtonDown += lbl_PreviewMouseLeftButtonDown;
            label.Content = draggedItem.Content;
            label.Tag = draggedItem.Tag != null ? draggedItem.Tag.ToString() : null;

            // Establecer la posición del nuevo objeto en el canvas
            System.Windows.Point posicionRaton = e.GetPosition(canvas);
            Canvas.SetLeft(label, posicionRaton.X);
            Canvas.SetTop(label, posicionRaton.Y);

            // Agregar el nuevo elemento al canvas
            canvas.Children.Add(label);
        }

        //cargamos los datos en el canvas
        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            // Establece el tamaño del canvas
            if (canvas.Width == 0 && canvas.Height == 0)
            {
                /*canvas.Width = canvasWidth;
                canvas.Height = canvasHeight;*/
                Listbox_CargarPLantilla(rutaPlantilla);
                //return;
            }

            
        }

        private void btnPrevisualizar_Click(object sender, RoutedEventArgs e)
        {
            // Guardar la posición original del Canvas
            canvas.SetValue(Canvas.LeftProperty, 0.0);
            canvas.SetValue(Canvas.TopProperty, 0.0);

            double left = Canvas.GetLeft(canvas);
            double top = Canvas.GetTop(canvas);

            PrintDialog printDialog = new PrintDialog();

            // Posicionar el Canvas arriba a la izquierda
            canvas.HorizontalAlignment = HorizontalAlignment.Left;
            canvas.VerticalAlignment = VerticalAlignment.Top;
            canvas.Margin = new Thickness(12, 12, 6, 0);

            // Medimos el canvas para que se ajuste a la página imprimible
            // Establecer la escala a 1 antes de imprimir
            canvas.RenderTransform = new ScaleTransform(1, 1);
            canvas.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));

            // Obtenemos la anchura y altura del canvas
            /*double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;*/

            // Calculamos el número de copias necesarias en el eje X y el eje Y
            int numCopiasX = (int)Math.Ceiling(printDialog.PrintableAreaWidth / canvasWidth) - 1; //-1 para que la ultima columna se omita
            int numCopiasY = numCopias;

            // Creamos un nuevo canvas grande para contener todas las copias
            Canvas canvasCopias = new Canvas();
            if(tamImpresion == "A4")
            {
                canvasCopias.Width = 794.49;
                canvasCopias.Height = 1108.66;
            }
            
            int contEtiquetas = 1;

            // Añadimos las copias al canvas grande
            for (int i = 0; i < numCopiasY; i++)
            {
                for (int j = 0; j < numCopiasX; j++)
                {
                    if (contEtiquetas <= numCopias)
                    {
                        Canvas.SetLeft(canvas, j * canvasWidth);
                        Canvas.SetTop(canvas, i * canvasHeight);
                        canvasCopias.Children.Add(XamlReader.Parse(XamlWriter.Save(canvas)) as UIElement);
                        contEtiquetas++;
                    }
                }
            }

            // se muestra una ventana de previsualizacion
            Window ventanaPrevisualizacion = new Window();
            ventanaPrevisualizacion.Title = "Previsualización impresion";
            // Crear un objeto SolidColorBrush con el color deseado
            SolidColorBrush colorFondo = new SolidColorBrush(Colors.LightGray);

            // Crear un ScrollViewer y agregar el canvasCopias como contenido
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = canvasCopias;

            // Ajustar el tamaño del ScrollViewer para que se ajuste al tamaño del Canvas
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            ventanaPrevisualizacion.Width = canvasCopias.Width + 90;
            ventanaPrevisualizacion.Height = canvasCopias.Height;
            scrollViewer.MaxWidth = ventanaPrevisualizacion.Width;
            scrollViewer.MaxHeight = ventanaPrevisualizacion.Height;

            // Agregar el ScrollViewer a la ventana de previsualización
            ventanaPrevisualizacion.Content = scrollViewer;
            ventanaPrevisualizacion.ShowDialog();

            // Restaurar la posición original del Canvas
            canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            canvas.VerticalAlignment = VerticalAlignment.Stretch;
            canvas.Margin = new Thickness(0, 0, 0, 0);
            Canvas.SetLeft(canvas, left);
            Canvas.SetTop(canvas, top);

        }

        //este metodo imprime un canvas grande en el formato que haya elegio el usuario en la anterior ventana (A4)
        private void ImprimirCanvas()
        {
            // Guardar la posición original del Canvas
            canvas.SetValue(Canvas.LeftProperty, 0.0);
            canvas.SetValue(Canvas.TopProperty, 0.0);

            double left = Canvas.GetLeft(canvas);
            double top = Canvas.GetTop(canvas);

            PrintDialog printDialog = new PrintDialog();

            // Posicionar el Canvas arriba a la izquierda
            canvas.HorizontalAlignment = HorizontalAlignment.Left;
            canvas.VerticalAlignment = VerticalAlignment.Top;
            canvas.Margin = new Thickness(margenIzquierdo, margenSuperior, margenDerecho, margenInferior);

            // Medimos el canvas para que se ajuste a la página imprimible
            // Establecer la escala a 1 antes de imprimir
            canvas.RenderTransform = new ScaleTransform(1, 1);
            canvas.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));

            // Obtenemos la anchura y altura del canvas
            /*double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;*/

            // Calculamos el número de copias necesarias en el eje X y el eje Y
            int numCopiasX = (int)Math.Ceiling(printDialog.PrintableAreaWidth / canvasWidth) - 1; //-1 para que la ultima columna se omita
            int numCopiasY = numCopias;

            // Creamos un nuevo canvas grande para contener todas las copias
            Canvas canvasCopias = new Canvas();
            if (tamImpresion == "A4")
            {
                canvasCopias.Width = 794.49;
                canvasCopias.Height = 1108.66;
            }

            int contEtiquetas = 1;

            // Añadimos las copias al canvas grande
            for (int i = 0; i < numCopiasY; i++)
            {
                for (int j = 0; j < numCopiasX; j++)
                {
                    if (contEtiquetas <= numCopias)
                    {
                        Canvas.SetLeft(canvas, j * canvasWidth);
                        Canvas.SetTop(canvas, i * canvasHeight);
                        canvasCopias.Children.Add(XamlReader.Parse(XamlWriter.Save(canvas)) as UIElement);
                        contEtiquetas++;
                    }
                }
            }

            // Restaurar la posición original del Canvas
            canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            canvas.VerticalAlignment = VerticalAlignment.Stretch;
            canvas.Margin = new Thickness(0, 0, 0, 0);
            Canvas.SetLeft(canvas, left);
            Canvas.SetTop(canvas, top);

            if (printDialog.ShowDialog() == true)
            {
                // Establecer la escala a 1 antes de imprimir
                canvas.RenderTransform = new ScaleTransform(1, 1);

                // Obtener el tamaño de página imprimible
                Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                canvas.Measure(pageSize);

                PrintTicket printTicket = printDialog.PrintTicket;
                printTicket.CopyCount = 1; // este numero controla las hojas a imprimir

                // Imprimir el contenido del Canvas
                printDialog.PrintVisual(canvasCopias, "Imprimiendo");
            }

        }

        //boton de imprimir
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            if (tamImpresion != "")
            {
                ImprimirCanvas();
            }
            else
            {
                // Guardar la posición original del Canvas
                canvas.SetValue(Canvas.LeftProperty, 0.0);
                canvas.SetValue(Canvas.TopProperty, 0.0);

                double left = Canvas.GetLeft(canvas);
                double top = Canvas.GetTop(canvas);

                // Posicionar el Canvas arriba a la izquierda
                canvas.HorizontalAlignment = HorizontalAlignment.Left;
                canvas.VerticalAlignment = VerticalAlignment.Top;
                //canvas.Margin = new Thickness(12, 12, 0, 0);
                canvas.Margin = new Thickness(margenIzquierdo, margenSuperior, margenDerecho, margenInferior);

                // Abrir el diálogo de impresión
                PrintDialog prnt = new PrintDialog();
                if (prnt.ShowDialog() == true)
                {
                    // Establecer la escala a 1 antes de imprimir
                    canvas.RenderTransform = new ScaleTransform(1, 1);

                    // Obtener el tamaño de página imprimible
                    Size pageSize = new Size(prnt.PrintableAreaWidth, prnt.PrintableAreaHeight);
                    canvas.Measure(pageSize);

                    PrintTicket printTicket = prnt.PrintTicket;
                    printTicket.CopyCount = numCopias;

                    // Imprimir el contenido del Canvas
                    prnt.PrintVisual(canvas, "Imprimiendo");
                }

                // Restaurar la posición original del Canvas
                canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
                canvas.VerticalAlignment = VerticalAlignment.Stretch;
                canvas.Margin = new Thickness(0, 0, 0, 0);
                Canvas.SetLeft(canvas, left);
                Canvas.SetTop(canvas, top);
            }

        }

        //cuando se hace doble click en un label del canvas se abre la ventana de edicion
        private void lbl_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            labelModificar = (Label)sender;
            if (labelModificar != null)
            {
                var form = new Window1();
                form.tbCambiaTexto.Text = labelModificar.Content.ToString();
                form.lblPrincipal.Text = labelModificar.Content.ToString();
                form.lblPrincipal.FontFamily = labelModificar.FontFamily;
                form.lblPrincipal.FontSize = labelModificar.FontSize;
                form.lblPrincipal.FontWeight = labelModificar.FontWeight;
                form.lblPrincipal.FontStyle = labelModificar.FontStyle;
                form.lblPrincipal.Foreground = labelModificar.Foreground;
                form.tbLetra.Text = labelModificar.FontSize.ToString();
                form.ShowDialog();
            }
        }

        //click en el menu de guardar plantilla
        private void GuardarPlantilla_Click(object sender, RoutedEventArgs e)
        {
            //comprobamos si en el canvas hay etiquetas
            if (canvas.Children.Count > 0)
            {
                List<Etiqueta> etiquetas = new List<Etiqueta>();
                Etiqueta etiqueta = new Etiqueta();

                //obtenemos el tamaño del canvas
                Size tamañoCanvas = new Size(canvasWidth, canvasHeight);
                //iteramos sobre cada objeto del canvas y guardamos las propiedades en el objeto etiqueta
                foreach (Label l in canvas.Children)
                {
                    //creamos un objeto de tipo etiqueta donde almacenaremos las propiedades de cada etiqueta
                    etiqueta = new Etiqueta();

                    if (l.Tag != null)
                    {
                        etiqueta.Texto = l.Content.ToString();
                        etiqueta.TamFuente = l.FontSize;
                        etiqueta.Fuente = l.FontFamily.ToString();
                        etiqueta.Grosor = l.FontWeight;
                        etiqueta.Estilo = l.FontStyle;
                        etiqueta.Color = l.Foreground;
                        etiqueta.posX = Canvas.GetLeft(l);
                        etiqueta.posY = Canvas.GetTop(l);
                        etiqueta.Tag = l.Tag.ToString();
                        etiqueta.TamañoCanvas = tamañoCanvas;
                        etiqueta.Fondo = canvas.Background;
                    }

                    else
                    {
                        etiqueta.Texto = l.Content.ToString();
                        etiqueta.TamFuente = l.FontSize;
                        etiqueta.Fuente = l.FontFamily.ToString();
                        etiqueta.Grosor = l.FontWeight;
                        etiqueta.Estilo = l.FontStyle;
                        etiqueta.Color = l.Foreground;
                        etiqueta.posX = Canvas.GetLeft(l);
                        etiqueta.posY = Canvas.GetTop(l);
                        etiqueta.Tag = "Tag fallido";
                        etiqueta.TamañoCanvas = tamañoCanvas;
                        etiqueta.Fondo = canvas.Background;
                    }

                    etiquetas.Add(etiqueta);
                }

                //creamos el objeto serializer pasandole el tipo de nuestro objeto como parametro
                // Antes de la serialización, agregar el tipo MatrixTransform
                List<Type> xtraTypes = new List<Type>();
                XmlSerializer x = new XmlSerializer(typeof(List<Etiqueta>), xtraTypes.ToArray());

                // Creamos un cuadro de diálogo de guardado
                Microsoft.Win32.SaveFileDialog dialogoGuardar = new Microsoft.Win32.SaveFileDialog();

                // Establecemos el filtro de archivo para que solo se muestren archivos XML
                dialogoGuardar.Filter = "Archivos XML|*.xml";

                // Obtenemos el nombre de la plantilla, si esta vacio salimos de la funcion
                string nombreArchivo = Microsoft.VisualBasic.Interaction.InputBox("Introduca el nombre de la plantilla:", "Nombre de la plantilla");
                if (nombreArchivo == "")
                {
                    return;
                }


                else
                {
                    try
                    {
                        //obtenemos la ruta relativa, CurrentDirectory me devuelve la ruta del archivo.exe de la app, subimos tres niveles a la raiz del proyecto donde esta el ini
                        string rutaFicheroConfig = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"config.ini");
                        if(!File.Exists(rutaFicheroConfig))
                        {
                            rutaFicheroConfig = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TagFarma\config.ini");
                        }
                        //string rutaFicheroConfig = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\config.ini");
                        string ruta = ObtenerRuta(rutaFicheroConfig);

                        string[] archivosPlantillas = Directory.GetFiles(ruta, "*.xml");

                        //comprobamos que en el directorio plantillas hayan ficheros
                        if (archivosPlantillas.Length > 0)
                        {
                            foreach (string archivo in archivosPlantillas)
                            {
                                //iteramos sobre los ficheros y miramos si existe un fichero con el mismo nombre que se le ha dado el usuario
                                if (nombreArchivo == System.IO.Path.GetFileNameWithoutExtension(archivo))
                                {
                                    //advertimos al usuario de si quiere sobreescribir el fichero
                                    MessageBoxResult result = System.Windows.MessageBox.Show("Ya existe una plantilla con ese nombre, ¿Desea sobreeescribirla?", "AdvertenciaPlantilla", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                                    if (result == MessageBoxResult.Yes)
                                    {
                                        // Guardar el archivo en la ruta especificada
                                        using (var sw = new StringWriter())
                                        {
                                            using (XmlTextWriter writer = new XmlTextWriter(ruta + "\\" + nombreArchivo + ".xml", Encoding.UTF8) { Formatting = Formatting.Indented })
                                            {
                                                x.Serialize(writer, etiquetas);
                                                System.Windows.MessageBox.Show("Plantilla guardada");
                                                return;
                                            }
                                        }
                                    }
                                    //si no quiere sobreescribir salimos de la funcion
                                    else
                                        return;
                                }

                            }//fin foreach

                        }

                        // Guardar el archivo en la ruta especificada
                        using (var sw = new StringWriter())
                        {
                            using (XmlTextWriter writer = new XmlTextWriter(ruta + "\\" + nombreArchivo + ".xml", Encoding.UTF8) { Formatting = Formatting.Indented })
                            {
                                x.Serialize(writer, etiquetas);
                                System.Windows.MessageBox.Show("Plantilla guardada en " + ruta);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Error -> " + ex.Message);
                    }

                }

            }

            else
            {
                System.Windows.MessageBox.Show("No se puede guardar la plantilla ya que no hay etiquetas en el canvas");
            }
        }

        //metodo auxiliar que nos sirve para leer el fichero config.ini y ver si la ruta esta en null o no, si esta null se guarda en el directorio de Documents de Windows
        public static string ObtenerRuta(string rutaFicheroConfig)
        {
            string ruta;

            string[] lineas = File.ReadAllLines(rutaFicheroConfig);
            foreach (string linea in lineas)
            {
                // Buscar la línea que comienza con "Documents"
                if (linea.StartsWith("Documents"))
                {
                    // Separar la línea en dos partes utilizando el caracter igual (=) como separador
                    string[] partes = linea.Split('=');

                    // Quedarse con la segunda parte que contiene el valor
                    if (partes.Length > 1)
                    {
                        ruta = partes[1].Trim();

                        if (ruta == "null")
                        {
                            //ruta = @"C:\Users\USUARIO\Documents\Plantillas\";
                            ruta = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"CPFarma\\documents\\PlantillaEtiquetas");
                            //si no existe la carpeta de Plantillas la crea
                            if (!Directory.Exists(ruta))
                            {
                                Directory.CreateDirectory(ruta);
                            }
                            rutaPlantillas = ruta;
                            return ruta;
                        }

                        //si la ruta no es null devuelvo la rutaPlantilla, que es la que hay en el config.ini
                        if (!Directory.Exists(ruta + "\\PlantillaEtiquetas"))
                        {
                            try
                            {
                                Directory.CreateDirectory(ruta + "\\PlantillaEtiquetas");
                                rutaPlantillas = ruta;
                                return rutaPlantillas + "\\PlantillaEtiquetas";
                            }
                            catch(Exception ex)
                            {
                                System.Windows.MessageBox.Show("Error: " + ex.ToString());
                            }
                            
                        }
                        else
                        {
                            rutaPlantillas = ruta;
                            return rutaPlantillas + "\\PlantillaEtiquetas";
                        }

                    }
                }
            }
            return null;
        }

        //boton de cargar plantilla del menu
        private void CargarPlantilla_Click(object sender, RoutedEventArgs e)
        {
            List<Etiqueta> etiquetas;

            //VARIABLES DEL RESULTADO DE LA CONSULTA SOBRE ORDENESPREP_VIEW
            string proced = "";
            int idproced = 0;
            float cantidad = 0;
            string unidades = "";
            string fechaelab = "";
            string fechacaduc = "";
            string comentariotag = "";
            string paciente = "";
            string soli = "";
            string personal = "";
            string supervisor = "";
            string numorden = "";
            int idproc = 0;
            string comentarioetiqueta = "";
            int conservacion = 0;
            string infoConservacion = "No especif.";


            //VARIABLES DEL RESULTADO DE LA CONSULTA SOBRE ORDENESPREP2
            string nombre = "";
            string present = "";
            string csspp = "";
            float cant = 0;
            float cantidadcalc = 0;
            float cantpresent = 0;
            float cantpresentreal = 0;
            string csppresent = "";
            string lote1 = "";
            string lote2 = "";
            string numlote1 = "";
            string nombre_componente = "";
            string unidad = "";
            string componente = "";
            double cantid = 0;
            int viaadmin = 0;
            string viaadm = "";
            string nhc = "";

            float cantidadEnvasada = 0;
            float contenido = 0;

            XmlSerializer serializer = new XmlSerializer(typeof(List<Etiqueta>));

            // Creamos un cuadro de diálogo de guardado
            Microsoft.Win32.OpenFileDialog dialogoAbrir = new Microsoft.Win32.OpenFileDialog();
            if (Directory.Exists(rutaPlantilla))
            {
                dialogoAbrir.InitialDirectory = rutaPlantilla;
            }

            // Establecemos el filtro de archivo para que solo se muestren archivos XML
            dialogoAbrir.Filter = "Archivos XML|*.xml";

            // Si el usuario ha elegido un archivo y ha pulsado el botón "Guardar"
            if (dialogoAbrir.ShowDialog() == true)
            {
                canvas.Children.Clear();

                rutaArchivo = dialogoAbrir.FileName;

                using (FileStream fileStream = new FileStream(rutaArchivo, FileMode.Open))
                {
                    etiquetas = (List<Etiqueta>)serializer.Deserialize(fileStream);
                }

                foreach (Etiqueta etiqueta in etiquetas)
                {
                    // Creamos un nuevo objeto Label
                    Label label = new Label();

                    label.MouseDown += Label_MouseDown; //agregamos evento para cuando se hace click y se mantiene en el label
                    label.MouseMove += Label_MouseMove; //agregamos evento para que se pueda mover el label por el canvas
                    label.MouseUp += Label_MouseUp;     //agregamos evento para cuando se suelta el click y se ha terminado de arrastrar el elemento
                    label.MouseDoubleClick += lbl_DoubleClick;
                    label.PreviewMouseLeftButtonDown += lbl_PreviewMouseLeftButtonDown;

                    string tagAux = etiqueta.Tag;

                    switch (tagAux)
                    {
                        default: label.Content = etiqueta.Texto; break;
                    }

                    //label.Content = etiqueta.Texto;
                    label.FontSize = etiqueta.TamFuente;
                    label.FontFamily = new System.Windows.Media.FontFamily(etiqueta.Fuente);
                    label.FontWeight = etiqueta.Grosor;
                    label.FontStyle = etiqueta.Estilo;
                    label.Foreground = etiqueta.Color;
                    label.Tag = etiqueta.Tag;

                    // Posicionamos el objeto Label en el canvas
                    Canvas.SetLeft(label, etiqueta.posX);
                    Canvas.SetTop(label, etiqueta.posY);

                    // Añadimos el objeto Label al canvas
                    canvas.Children.Add(label);

                    // Asignar el tamaño del canvas a partir de la etiqueta
                    canvas.Width = etiqueta.TamañoCanvas.Width;
                    canvas.Height = etiqueta.TamañoCanvas.Height;
                    canvasHeight = (int)canvas.Height;
                    canvasWidth = (int)canvas.Width;
                    canvas.Background = etiqueta.Fondo;

                }
            }

        }

        //cuando se hace cargar plantilla desde la ventana de TamCanvas
        public void Listbox_CargarPLantilla(string ruta)
        {
            List<Etiqueta> etiquetas;

            //deserializamos el xml que haya elegido el usuario y guardamos sus etiquetas en el List declarado como etiqetas
            XmlSerializer serializer = new XmlSerializer(typeof(List<Etiqueta>));
            using (FileStream fileStream = new FileStream(ruta, FileMode.Open))
            {
                etiquetas = (List<Etiqueta>)serializer.Deserialize(fileStream);
            }

            // Limpiar el canvas antes de cargar las etiquetas
            canvas.Children.Clear();

            //iteramos sobre cada etiqueta, guardamos los resultados de la consulta en diferentes variables y en funcion del tag que tenga se carga la etiqueta
            foreach (Etiqueta etiqueta in etiquetas)
            {
                // Creamos un nuevo objeto Label junto a sus eventos
                Label label = new Label();
                label.MouseDown += Label_MouseDown; //agregamos evento para cuando se hace click y se mantiene en el label
                label.MouseMove += Label_MouseMove; //agregamos evento para que se pueda mover el label por el canvas
                label.MouseUp += Label_MouseUp;     //agregamos evento para cuando se suelta el click y se ha terminado de arrastrar el elemento
                label.MouseDoubleClick += lbl_DoubleClick;
                label.PreviewMouseLeftButtonDown += lbl_PreviewMouseLeftButtonDown;

                string tagAux = etiqueta.Tag;

                switch (tagAux)
                {
                    default: label.Content = etiqueta.Texto; break;
                }

                //label.Content = etiqueta.Texto;
                label.FontSize = etiqueta.TamFuente;
                label.FontFamily = new System.Windows.Media.FontFamily(etiqueta.Fuente);
                label.FontWeight = etiqueta.Grosor;
                label.FontStyle = etiqueta.Estilo;
                label.Foreground = etiqueta.Color;
                label.Tag = etiqueta.Tag;

                // Posicionamos el objeto Label en el canvas
                Canvas.SetLeft(label, etiqueta.posX);
                Canvas.SetTop(label, etiqueta.posY);

                // Añadimos el objeto Label al canvas
                canvas.Children.Add(label);

                // Asignar el tamaño del canvas a partir de la etiqueta
                canvas.Width = etiqueta.TamañoCanvas.Width;
                canvas.Height = etiqueta.TamañoCanvas.Height;
                canvasHeight = (int)canvas.Height;
                canvasWidth = (int)canvas.Width;

                // Asignar el color del fondo
                canvas.Background = etiqueta.Fondo;
            }
        }

        //boton que elimina los labels del canvas
        private void btnLimpiarCanvas_Click(object sender, RoutedEventArgs e)
        {
            if (canvas.Children.Count > 0)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Está seguro de que desea eliminar todo el texto de la etiqueta?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    canvas.Children.Clear();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("La etiqueta ya está vacía");
            }
        }

        //este evento controla qué label del canvas esta seleccionada, asi cuando se pulse la tecla de suprimir lo elimine del canvas
        private void lblNombreProcedimiento_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                var label = e.Source as Label;
                if (label != null)
                {
                    canvas.Children.Remove(label);
                }
            }
        }

        //metodo que permite eliminar del canvas el label que se ha seleccionado
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Suprimir y si hay un label seleccionado
            if (e.Key == Key.Delete && labelSeleccionada != null)
            {
                // Eliminar el label del canvas y establecer el label seleccionado a null
                canvas.Children.Remove(labelSeleccionada);
                labelSeleccionada = null;
            }
        }

        //esta funciona se encarga de seleccionar un label del canvas y establece el focus en dicho label para que despues pueda eliminarse si se pulsa suprimir
        private void lbl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Establecer el label seleccionado y darle foco
            labelSeleccionada = (Label)sender;
            Keyboard.Focus(labelSeleccionada);
        }

        //este evento se encarga de hacer zoom al canvas usando la ruleta del raton
        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //obtenemos el centro del canvas, asi el zoom se hace desde el centro
            Point center = new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
            double zoom = e.Delta > 0 ? 1.1 : 0.9;

            // Obtenemos la escala
            double currentScale = canvas.RenderTransform.Value.M11;

            // Limitamos el zoom a la mitad del tamaño original del canvas, asi se evita que se reduzca el canvas al maximo
            if (zoom < 1 && currentScale <= 0.5)
            {
                return;
            }

            // Aplica la transformacion de la escala para hacer zoom
            ScaleTransform scaleTransform = new ScaleTransform(zoom, zoom, center.X, center.Y);
            canvas.RenderTransform = new TransformGroup() { Children = { canvas.RenderTransform, scaleTransform } };
        }

        //metodo para buscar en la listbox, se le pasa como parámetro el término a buscar y la función devuelve un nuevo listbox
        private List<object> FiltrarListBox(ListBox listBox, string searchTerm)
        {
            var items = listBox.Items;
            var filteredItems = new List<object>();

            int index = 0;
            foreach (var item in items)
            {
                if (item.ToString().ToLower().Contains(searchTerm))
                {
                    filteredItems.Add(item);

                    if (index + 1 < items.Count)
                    {
                        filteredItems.Add(items[index + 1]);
                    }
                }

                index++;
            }

            // Eliminar duplicados
            filteredItems = filteredItems.Distinct().ToList();

            return filteredItems;
        }



        //este metodo se encarga de buscar en las listbox
        //este metodo se encarga de buscar en las listbox
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //obtenemos el string en minusculas
            string searchTerm = tbSearch.Text.ToLower();

            //comprobamos si está vacio, si lo esta se muestran los items de los listBox al completo
            if (string.IsNullOrEmpty(tbSearch.Text))
            {
                listBoxProced.ItemsSource = items;
            }

            //si no está vacío se procede a la búsqueda, llamando al metodo FiltrarListBox, pasando el listbox y el termino a buscar como param
            else
            {
                var filteredItems1 = FiltrarListBox(listBoxProced, searchTerm);
                listBoxProced.ItemsSource = filteredItems1; //con ItemsSource cambiamos los items de la listbox que ya habian por los nuevos encontrados
            }
        }


        //funcion que permite limitar los caracteres que se puedan introducir en el textBox a solo números
        private void tbNumOrden_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        //boton de cerrar programa
        private void CerrarPrograma_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //boton de nuevo
        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            var ventanaTamCanvas = new TamCanvas();
            if (ventanaTamCanvas.ShowDialog() == true)
            {
                canvasWidth = ventanaTamCanvas.CanvasWidth;
                canvasHeight = ventanaTamCanvas.CanvasHeight;
                canvas.Width = canvasWidth;
                canvas.Height = canvasHeight;
                numCopias = ventanaTamCanvas.NumCopias;
                PreviewKeyDown += OnPreviewKeyDown;

                if (ventanaTamCanvas.PlantillaSeleccionada == true)
                {
                    rutaPlantilla = ventanaTamCanvas.rutaPlantilla;
                    Listbox_CargarPLantilla(rutaPlantilla);
                }
            }
        }

        //funcion que permite cambiar el color de fondo del canvas
        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                canvas.Background = new SolidColorBrush(e.NewValue.Value); // Actualizar el color de fondo del canvas
            }
        }

        //funcion que abre la ventana donde se encuentra el elemento pdfViewer para leer el manual de usuario
        private void ManualUsuario_Click(object sender, RoutedEventArgs e)
        {
            var windowManual = new WindowManual();
            if (windowManual.ShowDialog() == true)
            {

            }
        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            Window2 add = new Window2();
            add.ShowDialog();

            Label label = new Label();
            label.MouseDown += Label_MouseDown; //agregamos evento para cuando se hace click y se mantiene en el label
            label.MouseMove += Label_MouseMove; //agregamos evento para que se pueda mover el label por el canvas
            label.MouseUp += Label_MouseUp;     //agregamos evento para cuando se suelta el click y se ha terminado de arrastrar el elemento
            label.MouseDoubleClick += lbl_DoubleClick; //agregamos evento para cuando se hace click en el label y se abre la ventana de edicion
            label.PreviewMouseLeftButtonDown += lbl_PreviewMouseLeftButtonDown;
            label.Content = add.textoLabel;
            label.Tag = "TagLabelAñadida";

            Canvas.SetLeft(label, 0);
            Canvas.SetTop(label, 0);
            // Agregar el nuevo elemento al canvas
            canvas.Children.Add(label);
        }

        //metodo por si fuera necesario copiar el fichero de config a la carpeta de TagFarma
        public void CopiarConfig()
        {
            //cogemos el config ini que está al nivel de farmacotecnia.exe
            string rutaConfig = System.IO.Path.Combine(Environment.CurrentDirectory, "config.ini");
            string[] lineas = File.ReadAllLines(rutaConfig);
            File.WriteAllLines(System.IO.Path.Combine(Environment.CurrentDirectory, @"TagFarma\config.ini"),lineas);
        }

        //Click en personalizar margenes
        private void itemMargenes_Click(object sender, RoutedEventArgs e)
        {
            WindowMargenes ventana = new WindowMargenes();
            ventana.ShowDialog();
            margenIzquierdo = ventana.margenIzquierdo;
            margenSuperior = ventana.margenSuperior;
            margenDerecho = ventana.margenDerecho;
            margenInferior = ventana.margenInferior;
        }
    }
 }
    