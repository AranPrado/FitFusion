using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FitFusion.Models
{
    public class UsuarioModel
    {
        [Key]
        public int UserID { get; set; }

        public string AspNetUserID { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string? SobreNome {get; set;}

        [Required]
        public string Email { get; set; }

        public float Peso { get; set; }

        public int Idade { get; set; }

        public float Altura { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataCriacao { get; set; }

        //Relacionamentos//

        //Usuario para treino


        public ICollection<TreinoModel> Treinos { get; set; }

        //Cargos para usuarios


    }
}