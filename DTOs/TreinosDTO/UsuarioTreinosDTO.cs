namespace FitFusion.DTOs.TreinosDTO
{
    public class UsuarioTreinosDTO
    {
        public int UserID { get; set; }
        public string NomeUsuario { get; set; }
        public List<TreinoComExercicioDTO> Treinos { get; set; }
    }
}