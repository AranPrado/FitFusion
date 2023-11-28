using System.ComponentModel.DataAnnotations;
using FitFusion.Models;

namespace FitFusion.DTOs.TreinosDTO
{
    public class TreinoComExercicioDTO
    {
        public TreinoComExercicioDTO()
        {
            DataCriacao = DateTime.Now;
        }
        public int TreinoID { get; set; }
        public string NomeTreino { get; set; }
        public string DescricaoTreino { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataCriacao { get; set; }
        public List<ExerciciosModel> Exercicios { get; set; }
    }
}