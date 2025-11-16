using ClinicaMedica2025.Models;
using System;

namespace ClinicaMedica2025.Models
{
    public class Paciente : Persona
    {
        public Domicilio domicilio { get; set; }
        public Afiliado obraSocial { get; set; }
    }
}