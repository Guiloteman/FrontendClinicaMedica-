using ClinicaMedica2025.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{


    public class ComparadorIngreso : IComparer<Ingreso>
    {
        private Dictionary<string, Nivel> niveles;

        public ComparadorIngreso()
        {
            niveles = Nivel.ObtenerNiveles(); // Asumiendo que es un método estático
        }

        public int Compare(Ingreso x, Ingreso y)
        {
            if (!niveles.ContainsKey(x.nivel.ToString()) || !niveles.ContainsKey(y.nivel.ToString()))
                return 0; // No se puede comparar si no se encuentra el nivel

            var nivelX = niveles[x.nivel.ToString()];
            var nivelY = niveles[y.nivel.ToString()];

            // Primero comparamos por nivel de emergencia (menor valor = mayor prioridad)
            int prioridadComparacion = nivelX.NivelEmergencia.CompareTo(nivelY.NivelEmergencia);
            if (prioridadComparacion != 0)
                return prioridadComparacion;

            // Si tienen la misma prioridad, comparamos por tiempo máximo de espera
            int esperaComparacion = nivelX.DuracionMaxEspera.CompareTo(nivelY.DuracionMaxEspera);
            if (esperaComparacion != 0)
                return esperaComparacion;

            // Finalmente, si todo es igual, comparamos por hora de llegada
            return x.fechaIngreso.CompareTo(y.fechaIngreso);
        }
    }
}