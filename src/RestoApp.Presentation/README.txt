====================================================================
CAPA DE PRESENTACIÓN (RestoApp.Presentation)
====================================================================
Propósito:
Esta capa contiene toda la interfaz gráfica de usuario (UI) desarrollada con Avalonia UI en C#.
Aplica de forma estricta el patrón de diseño MVVM (Model - View - ViewModel).

REGLAS DE ARQUITECTURA:
1. No colocar consultas a la base de datos ni SQL en esta capa.
2. Las Vistas (Views) se comunican EXCLUSIVAMENTE con los ViewModels a través de Data Binding ({Binding ...}).
3. Las Vistas NO llaman directamente a los Servicios de datos.

Estructura interna:
- /Views       : Archivos XAML y su Code-Behind (.axaml y .axaml.cs).
- /ViewModels  : Clases que manejan la lógica de la UI y los datos a mostrar.
- /Converters  : Convertidores para transformar datos antes de dibujarlos en XAML.
