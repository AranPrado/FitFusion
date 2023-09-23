using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitFusion.Models
{
    public class TreinoModel
    {
        [Key]
        public int TreinoID { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatorio")]
        [MaxLength(80)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set;}

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataCriacao { get; set; }

        //Relacionamento para o usuario

        public int UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }

        //Fim

        //Relacionamento para os Exercicios

        public ICollection<ExerciciosModel> Exercicios { get; set; }

    }
}