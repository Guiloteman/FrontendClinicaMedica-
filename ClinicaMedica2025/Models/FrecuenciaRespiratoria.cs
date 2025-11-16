namespace ClinicaMedica2025.Models
{
    public class FrecuenciaRespiratoria : Frecuencia
    {
        public double valor;
        public FrecuenciaRespiratoria(double valor) : base(valor)
        {
            this.valor = valor;
        }
    }
}