namespace FitFusion.DTOs.AdminsDTO
{
    public class RecuperarUsuariosDTO
    {
        public int UserID { get; set; }

        public string AspNetUserID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public float Peso { get; set; }

        public int Idade { get; set; }

        public float Altura { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}