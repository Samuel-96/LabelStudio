using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace ProyectoEtiquetas
{
    [Serializable]
    [XmlInclude(typeof(MatrixTransform))]
    [XmlInclude(typeof(SolidColorBrush))]
    public class Etiqueta
    {
        public string Texto { get; set; }
        public double TamFuente { get; set; }
        public string Fuente { get; set; }
        public FontWeight Grosor { get; set; }
        public FontStyle Estilo { get; set; }
        public Brush Color { get; set; }
        public Brush Fondo { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }

        // Esta propiedad no será serializada, para evitar problemas de deserialización
        [XmlIgnore]
        public string Tag { get; set; }

        // Esta propiedad se serializará como un elemento en el archivo XML
        [XmlElement("Tag")]
        public XElement TagElement
        {
            get { return new XElement("Tag", Tag); }
            set { Tag = value.Value; }
        }

        public Size TamañoCanvas { get; set; }
    }

}
