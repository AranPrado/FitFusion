using FitFusion.Constants;
using FitFusion.Database;
using FitFusion.DTOs.TreinosDTO;
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
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
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
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
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
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
        public async Task<ActionResult<TreinoModel>> CriarNovoTreino([FromBody] CriarTreino treinoDto)
        {
            try
            {
                // Crie uma instância de TreinoModel com base nos dados do DTO
                var treinoModel = new TreinoModel
                {
                    Nome = treinoDto.Nome,
                    Descricao = treinoDto.Descricao,
                    // Atribua outras propriedades, se necessário
                };

                // Adicione o treinoModel ao contexto do banco de dados
                _contexto.Treinos.Add(treinoModel);
                await _contexto.SaveChangesAsync();

                // Retorne o treinoModel criado
                return treinoModel;
            }
            catch (System.Exception)
            {
                // Trate exceções, se necessário
                throw;
            }
        }


        [HttpPut("Atualizar/{id}")]
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
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
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
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
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
        public async Task<ActionResult<IEnumerable<UsuarioTreinosDTO>>> ListarTreinosUsuario()
        {
            try
            {
                var usuarios = await _contexto.Usuarios
                    .Include(u => u.Treinos)
                        .ThenInclude(t => t.Exercicios)
                    .ToListAsync();

                var usuariosTreinosDTO = usuarios.Select(usuario => new UsuarioTreinosDTO
                {
                    UserID = usuario.UserID,
                    NomeUsuario = usuario.Nome,
                    Treinos = usuario.Treinos.Select(treino => new TreinoComExercicioDTO
                    {
                        TreinoID = treino.TreinoID,
                        NomeTreino = treino.Nome,
                        DescricaoTreino = treino.Descricao,
                        Exercicios = treino.Exercicios.ToList()
                    }).ToList()
                }).ToList();

                return usuariosTreinosDTO;
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        [HttpGet("Exercicios")]
        [Authorize(Roles = Role.Treinador + "," + Role.Admin)]
        public async Task<ActionResult<TreinoComExercicioDTO>> ListarExerciciosPorTreino(int treinoId)
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

                var treinoComExerciciosDTO = new TreinoComExercicioDTO
                {
                    TreinoID = treino.TreinoID,
                    NomeTreino = treino.Nome,
                    DescricaoTreino = treino.Descricao,
                    Exercicios = treino.Exercicios.ToList()
                };

                return treinoComExerciciosDTO;
            }
            catch (System.Exception)
            {
                throw;
            }
        }


    }
}