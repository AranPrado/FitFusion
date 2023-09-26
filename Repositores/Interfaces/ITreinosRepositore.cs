using System.Threading.Tasks;
using FitFusion.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitFusion.Repositores
{
    public interface ITreinosRepositore
    {
        Task<IEnumerable<TreinoModel>> ListarTodosTreinos();

        Task<ActionResult<TreinoModel>> ProcurarTreinoPorId(int id);

        Task<TreinoModel> CriarNovoTreino(TreinoModel treino);

        Task<ActionResult<TreinoModel>> AtualizarTreino(TreinoModel treino, int id);

        Task<ActionResult<bool>> DeletaTreino(int id);

        Task<ActionResult<IEnumerable<TreinoModel>>> ListarTreinosUsuario();

        Task<ActionResult<IEnumerable<ExerciciosModel>>> ListarExerciciosPorTreino(int treinoId);
    }
}