using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public abstract class Persona
    {
        public string Cuil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
    }
}