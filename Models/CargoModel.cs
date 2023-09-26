using System.ComponentModel.DataAnnotations;

namespace FitFusion.Models
{
    public class CargoModel
    {
        [Key]
        public int CargoID { get; set; }

        [Required(ErrorMessage = "O nome do cargo é obrigatório")]
        [MaxLength(50)]
        public string Nome { get; set; }

        public ICollection<UsuarioModel> Usuarios { get; set; }

    }
}