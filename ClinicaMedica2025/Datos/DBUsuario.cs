using ClinicaMedica2025.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ClinicaMedica2025.Datos
{
    public class DBUsuario
    {
        private static string CadenaSql = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\guile\\source\\repos\\ClinicaMedica2025\\ClinicaMedica2025\\App_Data\\Database1.mdf;Integrated Security=True";

        public static bool Registrar(UsuarioDTO usuario)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSql))
                {
                    SqlCommand cmd = new SqlCommand("InsertarUsuarioCompleto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cuil", usuario.Cuil);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre.ToUpper());
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido.ToUpper());
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Matricula", usuario.MatriculaId);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@Restablecer", usuario.Restablecer);
                    cmd.Parameters.AddWithValue("@Confirmado", usuario.Confirmado);
                    cmd.Parameters.AddWithValue("@Token", usuario.Token);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if(filasAfectadas > 0) 
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return respuesta;
        }

        public static UsuarioDTO Validar(string correo, string clave)
        {
            UsuarioDTO usuario = null;
            
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSql))
                {
                    string query = "SELECT u.Restablecer, u.Confirmado, u.Rol, d.matricula AS Matricula, p.cuil AS Cuil, p.nombre AS Nombre, p.apellido AS Apellido, p.email AS Email FROM Usuario u INNER JOIN Doctor d ON u.id_doctor = d.id_doctor INNER JOIN Persona p ON d.id_persona = p.id_persona WHERE p.email = @email AND u.Clave = @clave UNION SELECT u.Restablecer, u.Confirmado, u.Rol, e.matricula AS Matricula, p.cuil AS Cuil, p.nombre AS Nombre, p.apellido AS Apellido, p.email AS Email FROM Usuario u INNER JOIN Enfermera e ON u.id_enfermera = e.id_enfermera INNER JOIN Persona p ON e.id_persona = p.id_persona WHERE p.email = @email AND u.Clave = @clave;";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@email", correo);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                Cuil = dr["Cuil"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                MatriculaId = dr["Matricula"].ToString(),
                                Restablecer = (bool)dr["Restablecer"],
                                Confirmado = (bool)dr["Confirmado"],
                                Rol = dr["Rol"].ToString()
                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return usuario;
        }


        public static UsuarioDTO Obtener(string correo)
        {
            UsuarioDTO usuario = null;
            
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSql))
                {
                    string query = "SELECT u.Clave, u.Restablecer, u.Confirmado, u.Token, u.Rol, d.matricula, p.cuil, p.nombre, p.apellido, p.email FROM Usuario u INNER JOIN Doctor d ON u.id_doctor = d.id_doctor INNER JOIN Persona p ON d.id_persona = p.id_persona WHERE p.email = @correo;";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                Cuil = dr["cuil"].ToString(),
                                Nombre = dr["nombre"].ToString(),
                                Apellido = dr["apellido"].ToString(),
                                Email = dr["email"].ToString(),
                                MatriculaId = dr["matricula"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Restablecer = (bool)dr["Restablecer"],
                                Confirmado = (bool)dr["Confirmado"],
                                Token = dr["Token"].ToString(),
                                Rol = dr["Rol"].ToString()
                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return usuario;
        }


        public static bool RestablecerActualizar(int restablecer, string clave, string token)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSql))
                {
                    string query = @"update Usuario set " +
                        "Restablecer= @restablecer, " +
                        "Clave=@clave " +
                        "where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@restablecer", restablecer);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool Confirmar(string token)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSql))
                {
                    string query = @"update Usuario set " +
                        "Confirmado= 1 " +
                        "where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}