namespace MiProyecto.Models;
// Modelo para almacenar la información del usuario en sesión
// En palabras simples, es una plantilla que transporta los datos del usuario al sistema
public class UsuarioSesion
{
    public long Dni { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public bool EsAdmin { get; set; }
}