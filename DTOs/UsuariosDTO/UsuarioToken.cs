namespace FitFusion.DTOs
{
    public class UsuarioToken
    {
        public bool Autenticado { get; set; }

        public DateTime Expiration { get; set; }

        public string Token { get; set; }

        public string Message { get; set; }

        public string AspNetUserID { get; set; }

        

        
    }
}