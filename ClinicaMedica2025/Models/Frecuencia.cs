using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public abstract class Frecuencia
    {
        public double valor;
        public Frecuencia(double valor) 
        {
            if (GetValor(valor))
            {
                this.valor = valor;
            }
            else
            {
                new Exception("No puede ser valor negativo");
            }
        }

        public bool GetValor(double valor) 
        {
            bool respuesta = false;
            try
            {
                if (valor > 0)
                {
                    return respuesta = true;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.ToString());
            }
            return respuesta;
        }
    }
}