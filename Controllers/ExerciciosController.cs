using FitFusion.Constants;
using FitFusion.Database;
using FitFusion.DTOs.UsuariosDTO;
using FitFusion.Models;
using FitFusion.Repositores.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]


    public class ExerciciosController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public ExerciciosController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        [Authorize(Roles = Role.Treinador)]
        public async Task<IEnumerable<ExerciciosModel>> ListarTodosExercicios()
        {
            try
            {
                return await _contexto.Exercicios.ToListAsync();
            }
            catch (System.Exception)
            {

                throw new Exception("Ops... Ocorreu um erro");
            }
        }

        [HttpGet("{id}", Name = "ExercicioId")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<ExerciciosModel>> ProcurarExercicioPorId(int id)
        {
            try
            {
                var exercicio = await _contexto.Exercicios.FirstOrDefaultAsync(e => e.ExercicioID == id);

                if (exercicio == null)
                {
                    return new NotFoundObjectResult("Exercicio não encontrado");
                }

                return exercicio;
            }
            catch (System.Exception)
            {

                throw new Exception("Ops... Ocorreu um erro");
            }
        }

        [HttpPost("CriarExercicio")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ExerciciosModel> CriarNovoExercicio([FromBody] ExerciciosModel exercicio)
        {
            try
            {
                await _contexto.Exercicios.AddAsync(exercicio);
                await _contexto.SaveChangesAsync();

                return exercicio;
            }
            catch (System.Exception)
            {

                throw new Exception("Ops... Ocorreu um erro");
            }
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<ExerciciosModel>> AtualizarExercicio(ExerciciosModel exercicioAtualizado, int id)
        {
            try
            {
                var exercicioExistente = await _contexto.Exercicios.FirstOrDefaultAsync(e => e.ExercicioID == id);

                if (exercicioExistente == null)
                {
                    throw new Exception("Exercicio não encontrado");
                }

                // Atualize apenas os campos que foram especificados na solicitação de atualização
                if (!string.IsNullOrEmpty(exercicioAtualizado.Nome))
                {
                    exercicioExistente.Nome = exercicioAtualizado.Nome;
                }

                if (!string.IsNullOrEmpty(exercicioAtualizado.Descricao))
                {
                    exercicioExistente.Descricao = exercicioAtualizado.Descricao;
                }

                if (exercicioAtualizado.Peso != 0)
                {
                    exercicioExistente.Peso = exercicioAtualizado.Peso;
                }

                if (exercicioAtualizado.Repeticoes != 0)
                {
                    exercicioExistente.Repeticoes = exercicioAtualizado.Repeticoes;
                }

                if (exercicioAtualizado.Descanso != 0)
                {
                    exercicioExistente.Descanso = exercicioAtualizado.Descanso;
                }

                if (exercicioAtualizado.Series != 0)
                {
                    exercicioExistente.Series = exercicioAtualizado.Series;
                }

                if (exercicioAtualizado.Biset != exercicioExistente.Biset)
                {
                    exercicioExistente.Biset = exercicioAtualizado.Biset;
                }

                if (exercicioAtualizado.Drop != exercicioExistente.Drop)
                {
                    exercicioExistente.Drop = exercicioAtualizado.Drop;
                }

                await _contexto.SaveChangesAsync();

                return exercicioExistente;
            }
            catch (System.Exception)
            {
                throw new Exception("Ops... Ocorreu um erro");
            }
        }



        [HttpDelete("Deleta/{id}")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<bool>> DeletaExercicio(int id)
        {
            try
            {
                var exercicioExistente = await _contexto.Exercicios.FirstOrDefaultAsync(e => e.ExercicioID == id);

                if (exercicioExistente == null)
                {
                    return new NotFoundObjectResult("Exercicio não encontrado");
                }

                _contexto.Exercicios.Remove(exercicioExistente);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                throw new Exception("Ops... Ocorreu um erro");
            }
        }

        [HttpPost("DeletarExercicioDoTreinoUsuario")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult> DeletarExercicioDoTreinoUsuario(
    [FromBody] DeletarExercicioDoTreinoUsuarioDTO dadosDesvinculacao
)
        {
            try
            {
                // Verifique se o usuário com o AspNetUserID especificado existe
                var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(
                    u => u.AspNetUserID == dadosDesvinculacao.AspNetUserID
                );

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Verifique se o treino com o TreinoID especificado existe
                var treino = await _contexto.Treinos.FirstOrDefaultAsync(
                    t => t.TreinoID == dadosDesvinculacao.TreinoID
                );

                if (treino == null)
                {
                    return NotFound("Treino não encontrado");
                }

                // Verifique se o exercício com o ExercicioID especificado existe
                var exercicio = await _contexto.Exercicios.FirstOrDefaultAsync(
                    e => e.ExercicioID == dadosDesvinculacao.ExercicioID
                );

                if (exercicio == null)
                {
                    return NotFound("Exercício não encontrado");
                }

                // Verifique se o treino tem uma lista de exercícios (se não tiver, você pode criar uma)
                if (treino.Exercicios == null)
                {
                    treino.Exercicios = new List<ExerciciosModel>();
                }

                // Verifique se o exercício está vinculado ao treino do usuário
                var exercicioVinculado = treino.Exercicios.FirstOrDefault(e => e.ExercicioID == dadosDesvinculacao.ExercicioID);
                if (exercicioVinculado == null)
                {
                    return BadRequest("Este exercício não está vinculado ao treino do usuário.");
                }

                // Desvincule o exercício do treino do usuário
                treino.Exercicios.Remove(exercicioVinculado);
                await _contexto.SaveChangesAsync();

                return Ok("Exercício desvinculado do treino do usuário com sucesso.");
            }
            catch (System.Exception)
            {
                // Trate exceções, se necessário
                throw;
            }
        }


    }
}