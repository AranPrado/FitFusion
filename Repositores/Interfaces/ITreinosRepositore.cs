using System.Threading.Tasks;
using FitFusion.DTOs.TreinosDTO;
using FitFusion.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitFusion.Repositores
{
    public interface ITreinosRepositore
    {
        Task<IEnumerable<TreinoModel>> ListarTodosTreinos();

        Task<ActionResult<TreinoModel>> ProcurarTreinoPorId(int id);

        Task<ActionResult<TreinoModel>> CriarNovoTreino([FromBody] CriarTreino treinoDto);

        Task<ActionResult<TreinoModel>> AtualizarTreino(TreinoModel treino, int id);

        Task<ActionResult<bool>> DeletaTreino(int id);

        Task<ActionResult<IEnumerable<UsuarioTreinosDTO>>> ListarTreinosUsuario();

        Task<ActionResult<TreinoComExercicioDTO>> ListarExerciciosPorTreino(int treinoId);
    }
}