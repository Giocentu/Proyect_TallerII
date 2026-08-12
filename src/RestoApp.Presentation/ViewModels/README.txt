====================================================================
CARPETA: ViewModels (Modelos de Vista - Patrón MVVM)
====================================================================
Propósito:
Actúa como puente intermedio entre las Vistas XAML (UI) y la Capa de Negocio.

¿Qué archivos van aquí?
- Clases C# que representan el estado y la lógica de cada pantalla.
- Ejemplos: LoginViewModel.cs, DashboardViewModel.cs, PersonalViewModel.cs.

REGLAS PARA EL EQUIPO:
1. Las clases de esta carpeta deben heredar de `ObservableObject` (del paquete CommunityToolkit.Mvvm).
2. Usar la anotación `[ObservableProperty]` sobre los campos privados para generar automáticamente propiedades que notifican cambios a la interfaz visual (INotifyPropertyChanged).
3. Usar la anotación `[RelayCommand]` sobre los métodos para convertirlos en comandos vinculables a botones XAML (ICommand).
4. Los ViewModels reciben los Servicios de Negocio por el constructor (Inyección de Dependencias).
