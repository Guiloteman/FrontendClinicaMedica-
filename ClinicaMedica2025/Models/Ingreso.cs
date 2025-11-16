using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public class Ingreso
    {
        public Atencion atencion { get; set; }
        public Paciente paciente { get; set; }
        public string idObraSocial { get; set; }
        public Enfermera enfermera { get; set; }
        public Nivel nivel { get; set; }
        public EstadoIngreso estadoIngreso { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaIngreso { get; set; } = DateTime.Now;
        public Temperatura temperatura { get; set; }
        public TensionArterial tensionArterial { get; set; }
        public FrecuenciaCardiaca frecuenciaCardiaca { get; set; }
        public FrecuenciaRespiratoria frecuenciaRespiratoria { get; set; }
    }
}