using FitFusion.Models;

namespace FitFusion.DTOs
{
    public class TreinoExerciciosDTO
    {
        
        public int TreinoID { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set;}

        public ICollection<ExerciciosModel> Exercicios { get; set; }
    }
}