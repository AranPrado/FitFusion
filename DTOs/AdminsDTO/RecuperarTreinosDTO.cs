namespace FitFusion.DTOs.AdminsDTO
{
    public class RecuperarTreinosDTO
    {
        public int TreinoID { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set;}

        public DateTime DataCriacao { get; set; }

        public int? UsuarioId { get; set; }
    }
}