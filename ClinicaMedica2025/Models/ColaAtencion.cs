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

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            int i = 1;

            foreach (var ingreso in ingresos)
            {
                // 1. Accede a las propiedades 'Nombre' y 'NivelId' del objeto 'nivel'
                string prioridadNombre = (ingreso.nivel != null)
                                       ? $"{ingreso.nivel.Nombre} ({ingreso.nivel.NivelId})"
                                       : "Nivel no asignado";

                // 2. Usar las propiedades correctas del paciente
                string pacienteNombreCompleto = (ingreso.paciente != null)
                                              ? $"{ingreso.paciente.Nombre} {ingreso.paciente.Apellido}"
                                              : "Paciente Desconocido";

                // 3. Crear la línea de salida legible
                sb.AppendLine($"[#{i++}] Prioridad: {prioridadNombre} | Paciente: {pacienteNombreCompleto} | Ingreso: {ingreso.fechaIngreso.ToString("HH:mm:ss")}");
            }

            return sb.ToString();
        }
    }
}
