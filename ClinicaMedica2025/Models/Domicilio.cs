using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public class Domicilio
    {
        private string calle;
        private string numero;
        private string ciudad;
        private string provincia;

        public Domicilio(string calle, string numero, string ciudad, string provincia)
        {
            this.calle = calle;
            this.numero = numero;
            this.ciudad = ciudad;
            this.provincia = provincia;
        }
    }
}