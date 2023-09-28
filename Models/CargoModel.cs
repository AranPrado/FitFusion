using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitFusion.Models
{
    public class CargoModel
    {
        [Key]
        public int CargoID { get; set; }

        [Required(ErrorMessage = "O nome do cargo é obrigatório")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [JsonIgnore]
        public ICollection<UsuarioModel>? Usuarios { get; set; }

    }
}