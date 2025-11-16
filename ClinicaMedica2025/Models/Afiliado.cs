using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public class Afiliado
    {
        public ObraSocial obraSocial;
        private string numeroAfiliado;

        public Afiliado(int idObraSocial, string nombre, string numeroAfiliado)
        {
            if (obraSocial.ExistePorNombre(nombre))
            {
                obraSocial = new ObraSocial(idObraSocial, nombre);
                this.numeroAfiliado = numeroAfiliado;
            }
            else
            {
                new Exception("Obra Social inexistente");
            }
            
        }
    }

}