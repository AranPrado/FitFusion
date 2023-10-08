namespace FitFusion.DTOs.AdminsDTO
{
    public class RecuperarExerciciosDTO
    {
        public int ExercicioID { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public double Peso { get; set; }

        public int Repeticoes { get; set; }

        public int Descanso { get; set; }

        public int Series { get; set; }

        public bool Biset { get; set; }

        public bool Drop { get; set; }


        public DateTime DataCriacao { get; set; }

        public int? TreinoId { get; set; }
    }
}