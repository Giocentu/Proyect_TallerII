namespace RestoApp.Presentation.Models;

public class UserModel
{
    public string Dni { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string Rol { get; set; } = "Mozo"; // Dueño, Gerente, Recepcionista, Cajero, Mozo
    public bool ActivoEnRol { get; set; } = true;
}
