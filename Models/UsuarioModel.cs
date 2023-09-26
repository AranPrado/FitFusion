using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitFusion.Models
{
    public class UsuarioModel
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Senha { get; set; }

        public float Peso { get; set; }

        public int Idade { get; set; }

        public float Altura { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataCriacao { get; set; }

        //Relacionamentos//

        //Usuario para treino

        public ICollection<TreinoModel> Treinos { get; set; }

        //Cargos para usuarios

        // Adicione uma chave estrangeira para o cargo
        public int CargoID { get; set; }

        public CargoModel Cargo { get; set; }

    }
}