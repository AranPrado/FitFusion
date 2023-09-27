using System.Text.Json.Serialization;
using FitFusion.Models;

namespace FitFusion.DTOs
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }

        public float Peso { get; set; }

        public int Idade { get; set; }

        public float Altura { get; set; }

        public int? CargoID { get; set; }

        [JsonIgnore]
        public ICollection<TreinoModel>? Treinos { get; set; }


    }
}