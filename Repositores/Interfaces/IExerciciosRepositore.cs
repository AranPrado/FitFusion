using FitFusion.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitFusion.Repositores.Interfaces
{
    public interface IExerciciosRepositore
    {
        Task<IEnumerable<ExerciciosModel>> ListarTodosExercicios();

        Task<ActionResult<ExerciciosModel>> ProcurarExercicioPorId(int id);

        Task<ExerciciosModel> CriarNovoExercicio(ExerciciosModel exercicio);

        Task<ActionResult<ExerciciosModel>> AtualizarExercicio(ExerciciosModel exercicio, int id);

        Task<ActionResult<bool>> DeletaExercicio(int id);

        Task<ActionResult<IEnumerable<ExerciciosModel>>> ListarExerciciosTreino();
    }
}