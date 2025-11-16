using Org.BouncyCastle.Asn1.Cms;
using System.Collections.Generic;

namespace ClinicaMedica2025.Models
{
    public class Nivel
    {
        public int NivelId { get; set; }
        public string Nombre { get; set; }
        public int DuracionMaxEspera { get; set; }
        public NivelEmergencia NivelEmergencia { get; set; }

        // Método para corroborar nivel por nombre
        public static Dictionary<string, Nivel> ObtenerNiveles()
        {
            return new Dictionary<string, Nivel>
        {
            { "Critica", new Nivel { NivelId = 1, Nombre = "Crítica", DuracionMaxEspera = 0, NivelEmergencia = NivelEmergencia.Critica } },
            { "Emergencia", new Nivel { NivelId = 2, Nombre = "Emergencia", DuracionMaxEspera = 10, NivelEmergencia = NivelEmergencia.Emergencia } },
            { "Urgencia", new Nivel { NivelId = 3, Nombre = "Urgencia", DuracionMaxEspera = 30, NivelEmergencia = NivelEmergencia.Urgencia } },
            { "UrgenciaMenor", new Nivel { NivelId = 4, Nombre = "Urgencia Menor", DuracionMaxEspera = 60, NivelEmergencia = NivelEmergencia.UrgenciaMenor } },
            { "SinUrgencia", new Nivel { NivelId = 5, Nombre = "Sin Urgencia", DuracionMaxEspera = 120, NivelEmergencia = NivelEmergencia.SinUrgencia } },
        };
        }
    }
}