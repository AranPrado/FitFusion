using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitFusion.Models
{
    public class ExerciciosModel
    {
        [Key]
        public int ExercicioID { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatorio")]
        [MaxLength(80)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Range(1, 500)]
        public double Peso { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo Descanso deve ser um número inteiro positivo.")]
        public int Repeticoes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo Descanso deve ser um número inteiro positivo.")]
        public int Descanso { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo Descanso deve ser um número inteiro positivo.")]
        public int Series { get; set; }

        public bool Biset { get; set; }

        public bool Drop { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataCriacao { get; set; }

        //Relacionamento entre tabelas

        //Exercicio para treino

        public int TreinoId { get; set; }

        [JsonIgnore]
        public TreinoModel Treino { get; set; }

        //Fim

    }
}