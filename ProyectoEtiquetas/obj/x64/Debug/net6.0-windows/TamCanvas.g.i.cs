﻿#pragma checksum "..\..\..\..\TamCanvas.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0EE0DC37E7804F3855A36D0FA1159383D40C3428"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ProyectoEtiquetas;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ProyectoEtiquetas {
    
    
    /// <summary>
    /// TamCanvas
    /// </summary>
    public partial class TamCanvas : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAnchura;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAltura;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAnchura;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAltura;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstPlantillas;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblUsarPlantilla;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbMaterialAcon;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNumCopias;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\TamCanvas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNumCopias;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ProyectoEtiquetas;component/tamcanvas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\TamCanvas.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblAnchura = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lblAltura = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.tbAnchura = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbAltura = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\TamCanvas.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lstPlantillas = ((System.Windows.Controls.ListBox)(target));
            
            #line 35 "..\..\..\..\TamCanvas.xaml"
            this.lstPlantillas.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstPlantillas_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblUsarPlantilla = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.cbMaterialAcon = ((System.Windows.Controls.ComboBox)(target));
            
            #line 45 "..\..\..\..\TamCanvas.xaml"
            this.cbMaterialAcon.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbMaterial_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lblNumCopias = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.tbNumCopias = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\..\..\TamCanvas.xaml"
            this.tbNumCopias.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tb_NumCopias);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

