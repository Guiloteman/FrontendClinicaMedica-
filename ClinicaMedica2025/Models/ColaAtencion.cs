using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public class ColaAtencion
    {
        private List<Ingreso> ingresos = new List<Ingreso>();

        public void Encolar(Ingreso ingreso)
        {
            ingresos.Add(ingreso);
            ingresos.Sort(new ComparadorIngreso());
        }

        public Ingreso Atender()
        {
            if (ingresos.Count == 0) return null;

            Ingreso siguiente = ingresos[0];
            ingresos.RemoveAt(0);
            return siguiente;
        }

        public void MostrarCola()
        {
            foreach (var ingreso in ingresos)
            {
                Console.WriteLine($"{ingreso.descripcion} - {ingreso.estadoIngreso} - {ingreso.nivel.ToString()} - {ingreso.fechaIngreso}");
            }
        }
    }
}
