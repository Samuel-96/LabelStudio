   M S B A M L   `   `   `  ����[  WPatagames.Pdf.Wpf, Version=4.22.20.60, Culture=neutral, PublicKeyToken=60fd6cf9b15941cfjGclr-namespace:Patagames.Pdf.Net.Controls.Wpf;assembly=Patagames.Pdf.WpfPatagames.Pdf.Net.Controls.Wpf  5   	   |Pclr-namespace:Patagames.Pdf.Net.Controls.Wpf.ToolBars;assembly=Patagames.Pdf.Wpf'Patagames.Pdf.Net.Controls.Wpf.ToolBars  5   	    ProyectoEtiquetas5clr-namespace:ProyectoEtiquetasProyectoEtiquetas 5	   	   "   ProyectoEtiquetas.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   QWpfGclr-namespace:Patagames.Pdf.Net.Controls.Wpf;assembly=Patagames.Pdf.Wpf  5   	   _ToolbarsPclr-namespace:Patagames.Pdf.Net.Controls.Wpf.ToolBars;assembly=Patagames.Pdf.Wpf  5   	   +localclr-namespace:ProyectoEtiquetas  5	   	     � Title$  Inicio��5   	   $	��549��6   $
��1046��6%    � WindowStartupLocation$ CenterScreen=�5   	   .��5      ]� .+�5   
   o� $	��Top=�6   ���#045c8c.@�5      m� $��_Archivo��6   
�������.@�5      m� -   5        $��_Nuevo��5      
���   �6?   m� 5      -   5        $��_Guardar plantilla��5      
���   �6V   m� 5      -   5        $��_Cargar plantilla��5      
���   �6T   5      m� 5      $��	_Opciones��6   
�������.@�5      m� -   5        $��_Salir del programa��5      
���   �6U   m� 5      -   5        $��_Limpiar etiqueta��5      
���   �6U   m� 5      -   5         m�Name$ AddLabel��5      $��Añadir texto a etiqueta��6-   
���   �6a   m� 5      $,��&_Cambiar color de fondo de la etiqueta��6   
���   �.@�5      m� $��_Añadir texto��6   
���   �66   #  Xceed.Wpf.Toolkit.ColorPicker  5      -   5          Name$ colorPicker��5   '   !   AvailableColorsSortingMode$ HueSaturationBrightness=�5       $��Stretch=�5!      $	��Top=�5"      $	��100��5#      $
��Auto��5$      5%      5&      m� 5(      $��Ayuda��6   
�������.@�5)      m� -   5        $��Manual de usuario��5)      
���   �6R   5*      5+      � 5-   
   $
��Auto��6    � ColumnDefinitions 5.      �� 5/      $��* �6#   �� 51      $��5 �6#   �� 53      $	��250 �6#   .+�58      �� $��2��6   ���#0A659A$��0,0,5,0q�67   $��Bottom=�6H   $��54��6c   $��2��6o   �� 59      -	   5         ��Name$ scrollViewer��59      $
��Auto=�61   $
��Auto=�6V   .��5:      �� -
   5         ��Name$ bordeLimitesCanvas��5:      
���   �
�������$��1q�6`   $
��Left=�6t   $��10q�6�   ._�5;      �� -   5         ��Name$ canvas��5;      
�������$��	10,10,0,0q�6@   	 J� 	AllowDrop$
	 True��6f   
 J� SnapsToDevicePixels$

 True��6�   $	��500��6�   $	��420��6�   $
}�True��6�   .+�5=      �� -   5         ��Name$ lblNombreProcedimiento��5=       $��Nombre del procedimiento��6@   
 � Tag$ proced��6c   $
��Left=�5>   $   $	��Top=�6?    �� Left$ 64��6W   
 �� Top$ 19��6T  �� 5@      -   5        $ lblFechaCaducidad��5@       $��
Caducidad:��6;   $ lblFechaCaducidad��6P   $��Center=�5A   $   $	��Top=�6A   $ 16��6Y   $ 63��6j   $��10(�6)  �� 5C      -   5        $" lblFechaCaducidadPlaceholder��5C       $��placeholder��6F   $ 
fechacaduc��6\   $
��Left=�5D   $   $	��Top=�6?   $ 79��6W   $ 63��6h   $��10(�6'  �� 5F      -   5        $ 	lblViaAdm��5F       $��Vía administración:��63   $ lblViaAdmin��6R   $��Center=�5G   $   $	��Top=�6A   $	 127��6Y   $��10(�6  $ 13��6'  �� 5I      -   5        $ lblCantidad��5I       $��
Cantidad: ��65   $ lblCantidad��6J   $
��Left=�5J   $   $	��Top=�6?   $	 185��6W   $	 127��6i   $��10(�6)  �� 5L      -   5        $ lblNumOrden��5L       $ lblNumOrden��65   $��Número de orden: ��6G   $��Center=�6c   $
��Left=�6~   $	 193��6�   $ 63��6�   $��10(�6j   J� RenderTransformOrigin$ 0.511,0.567&�6x  �� 5N      -   5        $ lblNumOrdenPlaceholder��5N       $ numorden��6@   $��placeholder��6O   $	��Top=�6e   $
��Left=�6}   $	 292��6�   $ 63��6�   $��10(�6i  �� 5O      -   5        $ lblViadminPlaceholder��5O       $ viaadmin��6?   $��placeholder��6N   $	 106��6d   $	 127��6v   $
��Left=�66  $	��Top=�6Q  $��10(�6i  �� 5P      -   5        $ lblCantidadPlaceholder��5P       $ cantelabreal��6@   $��placeholder��6S   $	 231��6i   $	 127��6{   $
��Left=�6;  $	��Top=�6V  $��10(�6n  �� 5Q      -   5        $ lblPaciente��5Q       $ NombrePaciente��65   $��	Paciente:��6J   $
��Left=�6  $��Center=�6(  $��10(�6C  $ 61��6Q  $ 1��6b  �� 5S      -   5        $ componente1��5S       $��Componente1:��65   $ componente1��6M   $ 15��5T   %   $ 80��66   $
��Left=�6�   $	��Top=�6  $��10(�6(  �� 5V      -   5        $ componente2��5V       $��Componente2:��65   $ componente2��6M   $ 15��5W   %   $ 92��66   $
��Left=�6�   $��Center=�6  $��10(�6+  �� 5Y      -   5        $ componente3��5Y       $��Componente3:��65   $ componente3��6L   $ 15��5Z   %   $	 108��66   $��Center=�6�   $	��Top=�6  $��10(�6+  �� 5\      -   5        $ componente4��5\       $��Componente4:��65   $ componente4��6L   $	 251��5]   %   $ 80��67   $
��Left=�6�   $	��Top=�6  $��10(�6)  �� 5_      -   5        $ componente5��5_       $��Componente5:��65   $ componente5��6L   $	 251��5`   %   $ 92��67   $��Center=�6�   $	��Top=�6  $��10(�6+  �� 5b      -   5        $ componente6��5b       $��Componente6:��65   $ componente6��6L   $	 251��5c   %   $	 108��67   $��Center=�6�   $	��Top=�6  $��10(�6,  �� 5e      -   5        $ lblHospital��5e       $��Nombre hospital��65   $ lblHospital��6O   $	 231��5f   %   $ 1��67   $
��Left=�6�   $��Center=�6  $��10(�6+  $ 
0.506,0.61&�69  �� 5h      -   5        $ lblConservacion��5h       $��Conservación:��69   $ lblConservacion��6Q   $ 18��5i   %   $ 45��66   $
��Left=�6�   $	��Top=�6  $��10(�6(  �� 5k      -   5        $ lblObservaciones��5k       $��Observaciones:��6:   $ lblObservaciones��6S   $	 193��5l   %   $ 45��67   $
��Left=�6�   $��Center=�6  $��10(�6,  5n      5o      5p      �� 5s      $��1��6   $��5��6+   $��Stretch=�65   �� 5v      $��2��6   $��Right=�6+   $��0,0,0,59q�6B   .��5w      ��  �� Orientation$ Vertical=�6   .+�5y      �� $��	Buscar..:��6   $��0��60   $��Center=�6@   $��5q�6[   $
��Bold"�6f   �� 5z      -   5         ��Name$ tbSearch��5z      $��0��61   $	��200��6A   $��5q�6M   6X   �� 5{      -    5        $ buscarId��5{      $
��Bold"�6.   $��Buscar por ID��6@   �� 5|      -!   5        $ 
tbNumOrden��5|      $��90��6W   6b   �� 5}      .@�5~      �� $��Procedimientos��6"   
����d�.��5      �� -"   5         ��Name$ listBoxProced��5   &   ��5�   "   �� 5�   &    �� 
TargetType" ��6,   .�5�   *   -#   5        5�   '   5�      5�      �� 5�      $��Orden de preparación��6"   
����d�.��5�      �� -$   5        $ listBoxPrep��5�   &   ��5�   "   �� 5�   &   " ��6,   .�5�   *   -%   5        5�   '   5�      5�      �� 5�      $��Componentes��6"   
����d�.��5�      �� .��5�   "   �� $ Vertical=�6-   .+�5�   &   �� -&   5        $ listBoxComponentes��5�   .   ��5�   *   �� 5�   .   " ��64   .�5�   2   -'   5        5�   /   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      -(   5         ��Name$ btnImprimir��5�      $��2��6*   $��Imprimir��6:   $��0,0,20,5q�6M   $��Bottom=�6_   $��Right=�6z   $��30��6�   $��68��6�   $��
Nirmala UI)�6�   6�   �� 5�      -)   5        $ btnPrevisualizar��5�      $��2��6/   $��Previsualizar etiqueta��6?   $��	0,0,106,5q�6`   $��Bottom=�6s   $��Right=�6�   $��30��6�   $	��124��6�   $��
Nirmala UI)�6�   6�   5�      5�      5�      