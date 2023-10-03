using FitFusion.Database;
using FitFusion.Models;
using FitFusion.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{   
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]

    public class TreinoController : ITreinosRepositore
    {

        private readonly AppDbContext _contexto;

        public TreinoController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IEnumerable<TreinoModel>> ListarTodosTreinos()
        {
            try
            {
                return await _contexto.Treinos.ToListAsync();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TreinoModel>> ProcurarTreinoPorId(int id)
        {
            try
            {
                var TreinoId = await _contexto.Treinos.FirstOrDefaultAsync(t => t.TreinoID == id);

                if (TreinoId == null)
                {
                    return new NotFoundObjectResult("Treino não encontrado");
                }

                return TreinoId;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("CriarTreino")]
        [Authorize(Roles = "Admin")]
        public async Task<TreinoModel> CriarNovoTreino([FromBody] TreinoModel treino)
        {
            try
            {
                await _contexto.Treinos.AddAsync(treino);
                await _contexto.SaveChangesAsync();

                return treino;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpPut("Atualizar/{id}")]
        public async Task<ActionResult<TreinoModel>> AtualizarTreino(TreinoModel treinoAtualizado, int id)
        {
            try
            {
                var treinoExistente = await _contexto.Treinos.FirstOrDefaultAsync(t => t.TreinoID == id);

                if (treinoExistente == null)
                {
                    return new NotFoundObjectResult("Treino não encontrado");
                }

                if (!string.IsNullOrEmpty(treinoAtualizado.Nome))
                {
                    treinoExistente.Nome = treinoAtualizado.Nome;
                }

                if (!string.IsNullOrEmpty(treinoAtualizado.Descricao))
                {
                    treinoExistente.Descricao = treinoAtualizado.Descricao;
                }

                if (treinoAtualizado.UsuarioId.HasValue)
                {
                    treinoExistente.UsuarioId = treinoAtualizado.UsuarioId;
                }

                await _contexto.SaveChangesAsync();

                return treinoExistente;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpDelete("Deleta/{id}")]
        public async Task<ActionResult<bool>> DeletaTreino(int id)
        {
            try
            {
                var treinoExistente = await _contexto.Treinos.FirstOrDefaultAsync(t => t.TreinoID == id);

                if (treinoExistente == null)
                {
                    return new NotFoundObjectResult("Treino não encontrado");
                }

                _contexto.Treinos.Remove(treinoExistente);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpGet("Usuarios")]
        public async Task<ActionResult<IEnumerable<TreinoModel>>> ListarTreinosUsuario()
        {
            return await _contexto.Treinos.Include(u => u.Usuario).ToListAsync();
        }

        [HttpGet("Exercicios")]
        public async Task<ActionResult<IEnumerable<ExerciciosModel>>> ListarExerciciosPorTreino(int treinoId)
        {
            try
            {
                var treino = await _contexto.Treinos
                    .Include(t => t.Exercicios) // Carregue os exercícios relacionados com o treino
                    .FirstOrDefaultAsync(t => t.TreinoID == treinoId);

                if (treino == null)
                {
                    throw new Exception("Treino não encontrado");
                }

                var exercicios = treino.Exercicios.ToList();
                return exercicios;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}