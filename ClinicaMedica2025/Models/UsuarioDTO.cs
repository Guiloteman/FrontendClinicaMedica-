using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ClinicaMedica2025.Models
{
    public class UsuarioDTO
    {
        public string Cuil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string MatriculaId { get; set; }
        public int IdUsuario { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Restablecer { get; set; }
        public bool Confirmado { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
    }
}