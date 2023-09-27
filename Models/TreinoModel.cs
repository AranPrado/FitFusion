using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FitFusion.Models
{
    public class TreinoModel
    {

        public TreinoModel()
        {
            Exercicios = new Collection<ExerciciosModel>();
        }

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

        public int? UsuarioId { get; set; }
        
        [JsonIgnore]
        public UsuarioModel? Usuario { get; set; }

        //Fim

        //Relacionamento para os Exercicios
        [JsonIgnore]
        public ICollection<ExerciciosModel> Exercicios { get; set; }

    }
}