namespace ClinicaMedica2025.Models
{
    public class TensionArterial : Frecuencia
    {
        public Frecuencia frecuenciaSistolica;
        public Frecuencia frecuenciaDiastolica;
        public TensionArterial( double valor, Frecuencia frecuenciaSistolica, Frecuencia frecuenciaDiastolica) : base(valor)
        {
            this.frecuenciaSistolica = frecuenciaSistolica;
            this.frecuenciaDiastolica = frecuenciaDiastolica;
        }
    }
}