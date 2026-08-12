====================================================================
CARPETA: Views (Vistas XAML)
====================================================================
Propósito:
Almacena las pantallas, ventanas y controles de usuario de Avalonia UI.

¿Qué archivos van aquí?
- Ventanas (.axaml) y su archivo parcial de código (.axaml.cs).
- Ejemplos: MainWindow.axaml, DashboardWindow.axaml, PersonalWindow.axaml.

REGLAS PARA EL EQUIPO:
- El archivo .axaml define el diseño visual (botones, cajas de texto, colores, layouts).
- El archivo .axaml.cs (Code-Behind) debe estar LO MÁS LIMPIO POSIBLE. Solo debe contener el método InitializeComponent() y eventos exclusivamente de diseño UI (animaciones o foco de controles).
- Toda la lógica de clics y entrada de usuario debe gestionarse desde su correspondiente ViewModel mediante comandos ([RelayCommand]).
