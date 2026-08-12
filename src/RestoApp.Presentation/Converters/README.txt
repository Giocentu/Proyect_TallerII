====================================================================
CARPETA: Converters (Value Converters XAML)
====================================================================
Propósito:
Almacena clases C# que implementan la interfaz `IValueConverter` de Avalonia UI.

¿Qué archivos van aquí?
- Clases para convertir tipos de datos entre el ViewModel y la Vista XAML.
- Ejemplos: 
  * BoolToColorConverter.cs (convierte true/false a un color Verde/Rojo para el estado de una mesa).
  * DateTimeFormatterConverter.cs (formatea fechas para su presentación visual).

REGLAS PARA EL EQUIPO:
- Usar convertidores cuando XAML no pueda mostrar un valor directamente o requiera una transformación visual simple.
