using System.Threading.Tasks;
using FitFusion.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitFusion.Repositores
{
    public interface IUsuariosRepositore
    {
        Task<IEnumerable<UsuarioModel>> ListarTodosUsuarios();

        Task<ActionResult<UsuarioModel>> ProcurarUsuarioPorId(int id);

        Task<UsuarioModel> CriarNovoUsuario(UsuarioModel usuario);

        Task<ActionResult<UsuarioModel>> AtualizarUsuario(UsuarioModel usuario, int id);

        Task<ActionResult<bool>> DeletaUsuario(int id);

        // Método para listar os treinos e exercícios de um usuário específico
        Task<IEnumerable<TreinoModel>> ListarTreinosEExerciciosDeUsuario(int userId);
    }
}