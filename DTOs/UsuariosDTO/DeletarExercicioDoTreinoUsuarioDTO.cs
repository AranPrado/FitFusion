namespace FitFusion.DTOs.UsuariosDTO
{
    public class DeletarExercicioDoTreinoUsuarioDTO
    {
        public string AspNetUserID { get; set; } // O ID do usuário do ASP.NET
        public int TreinoID { get; set; } // O ID do treino
        public int ExercicioID { get; set; } // O ID do exercício
    }
}