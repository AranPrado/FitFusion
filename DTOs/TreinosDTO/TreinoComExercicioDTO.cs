using FitFusion.Models;

namespace FitFusion.DTOs.TreinosDTO
{
    public class TreinoComExercicioDTO
    {
        public int TreinoID { get; set; }
        public string NomeTreino { get; set; }
        public string DescricaoTreino { get; set; }
        public List<ExerciciosModel> Exercicios { get; set; }
    }
}