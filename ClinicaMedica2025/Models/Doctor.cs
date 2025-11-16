using ClinicaMedica2025.Datos;
using ClinicaMedica2025.Models;
using ClinicaMedica2025.Servicios;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaMedica2025.Models
{
    public class Doctor: Persona
    {
        public string MatriculaId { get; set; }
    }
}