using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FitFusion.Models;
using Microsoft.AspNetCore.Identity;

namespace FitFusion.DTOs
{
    public class UsuarioDTO
    {

        public int UserID { get; set; }

        public string Nome { get; set; }

        public string? SobreNome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        [NotMapped]
        public string ConfirmarSenha { get; set; }

        public float Peso { get; set; }

        public int Idade { get; set; }

        public float Altura { get; set; }

    
        public DateTime DataCriacao { get; set; }

        public ICollection<RoleIdDTO> Cargo { get; set; }

        [JsonIgnore]
        public ICollection<TreinoModel>? Treinos { get; set; }



    }
}