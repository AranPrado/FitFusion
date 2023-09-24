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
        public async Task<ActionResult<ExerciciosModel>> AtualizarExercicio(ExerciciosModel exercicio, int id)
        {
            try
            {
                var exercicioExistente = await _contexto.Exercicios.FirstOrDefaultAsync(e => e.ExercicioID == id);
    
                if (exercicioExistente == null)
                {
                    throw new Exception("Exercicio não encontrado");
                }
    
                exercicioExistente.Nome = exercicio.Nome;
                exercicioExistente.Descricao = exercicio.Descricao;
                exercicioExistente.Peso = exercicio.Peso;
                exercicioExistente.Repeticoes = exercicio.Repeticoes;
                exercicioExistente.Descanso = exercicio.Descanso;
                exercicioExistente.Series = exercicio.Series;
                exercicioExistente.Biset = exercicio.Biset;
                exercicioExistente.Drop = exercicio.Drop;
    
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

        //Preciso adicionar uma lista para recuperar todos os exercicios e em quais treinos eles estão. Exercicios => Treinos
        
    }
}