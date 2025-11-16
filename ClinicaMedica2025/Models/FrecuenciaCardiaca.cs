namespace ClinicaMedica2025.Models
{
    public class FrecuenciaCardiaca : Frecuencia
    {
        public double valor;
        public FrecuenciaCardiaca(double valor) : base(valor)
        {
            this.valor = valor;
        }
    }
}