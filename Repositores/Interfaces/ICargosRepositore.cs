using FitFusion.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitFusion.Repositores.Interfaces
{
    public interface ICargosRepositore
    {
        Task<IEnumerable<CargoModel>> ListarTodosCargos();

        Task<ActionResult<CargoModel>> ProcurarCargoPorId(int id);

        Task<CargoModel> CriarNovoCargo(CargoModel cargoModel);

        Task<ActionResult<CargoModel>> AtualizarCargo(CargoModel cargoModel, int id);

        Task<ActionResult<bool>> DeletarCargo(int id);

        Task<IEnumerable<UsuarioModel>> ListarCargosUsuarios();
    }
}