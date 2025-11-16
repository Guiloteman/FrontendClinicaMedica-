using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaMedica2025.Models
{
    public class ObraSocial
    {
        private static List<ObraSocial> social = new List<ObraSocial>();
        private int idObraSocial;
        private string nombre;

        public ObraSocial(int idObraSocial, string nombre)
        {
            ObraSocial ob1 = new ObraSocial(1, "OSDE");
            ObraSocial ob2 = new ObraSocial(2, "Swiss Medical");
            ObraSocial ob3 = new ObraSocial(3, "Prensa");
            this.idObraSocial = idObraSocial;
            this.nombre = nombre;
            social.Add(this);
            social.Add(ob1);
            social.Add(ob2);
            social.Add(ob3);
        }

        public bool ExistePorNombre(string nombreBuscado)
        {
            return social.Any(os => os.nombre == nombreBuscado);
        }
        
    }
}

