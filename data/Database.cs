// Puente entre la base de datos y la aplicación
using Microsoft.Data.SqlClient;
using MiProyecto.Models; // Importamos la carpeta models
using System;

namespace MiProyecto.Data;

public class Database
{
    // Cadena de conexión a tu Docker local
    private string connectionString = "Server=127.0.0.1,1433;Database=proyect_Resto;User Id=sa;Password=AlphaCentu123.;TrustServerCertificate=True;";
    public UsuarioSesion? Login(string usuario, string password)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                
                // Consulta que une Empleado + Persona + Rol
                string query = @"
                    SELECT e.dni_empleado, p.nombre, p.apellido, r.descripcion, r.permiso_admin 
                    FROM empleado e
                    INNER JOIN persona p ON p.dni = e.dni_empleado
                    INNER JOIN rol_empleado r ON r.id_rol = e.id_rol
                    WHERE p.dni = @user 
                    AND p.password = @pass 
                    AND e.activo_en_rol = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Convertimos el usuario a número (DNI)
                    if (long.TryParse(usuario, out long dniNumerico))
                    {
                         cmd.Parameters.AddWithValue("@user", dniNumerico);
                    }
                    else
                    {
                         return null; // Si escribió letras en el DNI, rechazamos
                    }

                    cmd.Parameters.AddWithValue("@pass", password);

                 using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UsuarioSesion
                        {
                            Dni = reader.GetInt64(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Rol = reader.GetString(3),
                            
                            // LEER EL BOOLEANO (BIT)
                            // Si en SQL es BIT, en C# es bool.
                            // GetBoolean(4) lee la columna 4 (r.permiso_admin)
                            EsAdmin = reader.GetBoolean(4) 
                        };
                    }
                }
            }
        }
            catch (Exception ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
            }
        }
        return null;
    }
}