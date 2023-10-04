using FitFusion.Database;
using FitFusion.Models;
using FitFusion.Repositores.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ExerciciosController : IExerciciosRepositore
    {
        private readonly AppDbContext _contexto;

        public ExerciciosController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
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


    }
}