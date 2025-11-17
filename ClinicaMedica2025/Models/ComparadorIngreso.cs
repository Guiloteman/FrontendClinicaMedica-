using ClinicaMedica2025.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    //public class ComparadorIngreso : IComparer<Ingreso>
    //{
    //    private Dictionary<string, Nivel> niveles;

    //    public ComparadorIngreso()
    //    {
    //        niveles = Nivel.ObtenerNiveles(); // Asumiendo que es un método estático
    //    }

    //    public int Compare(Ingreso x, Ingreso y)
    //    {
    //        if (!niveles.ContainsKey(x.nivel.ToString()) || !niveles.ContainsKey(y.nivel.ToString()))
    //            return 0; // No se puede comparar si no se encuentra el nivel

    //        var nivelX = niveles[x.nivel.ToString()];
    //        var nivelY = niveles[y.nivel.ToString()];

    //        // Primero comparamos por nivel de emergencia (menor valor = mayor prioridad)
    //        int prioridadComparacion = nivelX.NivelEmergencia.CompareTo(nivelY.NivelEmergencia);
    //        if (prioridadComparacion != 0)
    //            return prioridadComparacion;

    //        // Si tienen la misma prioridad, comparamos por tiempo máximo de espera
    //        int esperaComparacion = nivelX.DuracionMaxEspera.CompareTo(nivelY.DuracionMaxEspera);
    //        if (esperaComparacion != 0)
    //            return esperaComparacion;

    //        // Finalmente, si todo es igual, comparamos por hora de llegada
    //        return x.fechaIngreso.CompareTo(y.fechaIngreso);
    //    }
    //}

    public class ComparadorIngreso : IComparer<Ingreso>
    {
        private Dictionary<string, Nivel> niveles;

        public ComparadorIngreso()
        {
            // Se inicializa el diccionario de niveles
            niveles = Nivel.ObtenerNiveles();
        }

        public int Compare(Ingreso x, Ingreso y)
        {
            // 1. Obtener la clave (string) del NivelEmergencia
            //    Esto produce el nombre del enum (Ej: "Critica", "Emergencia")
            var claveX = x.nivel.NivelEmergencia.ToString();
            var claveY = y.nivel.NivelEmergencia.ToString();

            // 2. Verificar si las claves existen en el diccionario estático
            if (!niveles.ContainsKey(claveX) || !niveles.ContainsKey(claveY))
            {
                // Si el nivel no existe, no podemos comparar. Por seguridad, ordenamos por fecha.
                return x.fechaIngreso.CompareTo(y.fechaIngreso);
            }

            var nivelX = niveles[claveX];
            var nivelY = niveles[claveY];

            // A. Primero comparamos por NivelEmergencia (menor valor = mayor prioridad)
            //    Esto asegura que Triage 1 va antes que Triage 5.
            int prioridadComparacion = nivelX.NivelEmergencia.CompareTo(nivelY.NivelEmergencia);
            if (prioridadComparacion != 0)
                return prioridadComparacion;

            // B. Si la prioridad es igual, comparamos por hora de llegada (FIFO)
            //    La fecha más antigua debe ir primero (resultado negativo).
            return x.fechaIngreso.CompareTo(y.fechaIngreso);

            // Nota: La comparación por DuracionMaxEspera no es necesaria aquí si ya comparas por NivelEmergencia,
            // ya que ambos están intrínsecamente ligados a DuracionMaxEspera. Usar solo la fecha de ingreso
            // asegura un desempate correcto (FIFO) para niveles iguales.
        }
    }
}