using FitFusion.Database;
using FitFusion.DTOs;
using FitFusion.DTOs.TreinosDTO;
using FitFusion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{



    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]

    public class UsuarioController : ControllerBase
    {

        private readonly AppDbContext _contexto;

        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioController(AppDbContext contexto, UserManager<IdentityUser> userManager)
        {
            _contexto = contexto;
            _userManager = userManager;
        }

        [HttpGet("InformacoesUsuario/{id}")]
        public async Task<ActionResult<UsuarioModel>> ListaUsuarioId(string id)
        {
            var usuarioID = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

            if (usuarioID == null)
            {
                return new NotFoundObjectResult("Usuario não encontrado");
            }

            return usuarioID;
        }

        [HttpGet("TreinosComExercicios/{id}")]
        public async Task<ActionResult<IEnumerable<TreinoComExercicioDTO>>> ObterTreinosComExerciciosDoUsuario(string id)
        {
            try
            {
                // Verifica se o usuário com o ID especificado existe
                var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Obtém os treinos associados a esse usuário incluindo os exercícios
                var treinosComExercicios = await _contexto.Treinos
                    .Include(t => t.Exercicios) // Inclui os exercícios relacionados aos treinos
                    .Where(t => t.UsuarioId == usuario.UserID)
                    .ToListAsync();

                // Mapeia os treinos e exercícios para o DTO TreinoComExercicioDTO
                var treinosExerciciosDTO = treinosComExercicios.Select(treino => new TreinoComExercicioDTO
                {
                    TreinoID = treino.TreinoID,
                    NomeTreino = treino.Nome,
                    DescricaoTreino = treino.Descricao,
                    Exercicios = treino.Exercicios.ToList() // Lista de exercícios
                }).ToList();

                return treinosExerciciosDTO;
            }
            catch (System.Exception)
            {
                throw;
            }
        }




        [HttpPut("AtualizarUsuarioID/{id}")]
        public async Task<ActionResult<UsuarioModel>> AtualizarUsuarioId(string id, [FromBody] EditUsuarioDTO usuarioAtualizado)
        {
            var usuarioExistente = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

            if (usuarioExistente == null)
            {
                return new NotFoundObjectResult("Usuário não encontrado");
            }

            if (!string.IsNullOrEmpty(usuarioAtualizado.Nome))
            {
                usuarioExistente.Nome = usuarioAtualizado.Nome;
            }

            if (!string.IsNullOrEmpty(usuarioAtualizado.Email))
            {
                usuarioExistente.Email = usuarioAtualizado.Email;
            }

            if (usuarioAtualizado.Peso != null)
            {
                usuarioExistente.Peso = usuarioAtualizado.Peso;
            }

            if (usuarioAtualizado.Idade != null)
            {
                usuarioExistente.Idade = usuarioAtualizado.Idade;
            }

            if (usuarioAtualizado.Altura != null)
            {
                usuarioExistente.Altura = usuarioAtualizado.Altura;
            }

            // Certifique-se de atualizar outros campos, se necessário.

            await _contexto.SaveChangesAsync();

            return usuarioExistente;
        }

        [HttpDelete("DeletaUsuarioID/{id}")]
        public async Task<ActionResult<bool>> DeletarUsuario(string id)
        {
            var usuarioExistente = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _contexto.Usuarios.Remove(usuarioExistente);
            await _contexto.SaveChangesAsync();

            // Em seguida, você pode excluir o usuário da tabela AspNetUsers
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok("Usuário excluído com sucesso.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return NotFound("Usuário não encontrado na tabela AspNetUsers");
        }




    }
}