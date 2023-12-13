using FitFusion.Constants;
using FitFusion.Database;
using FitFusion.DTOs.TreinosDTO;
using FitFusion.DTOs.UsuariosDTO;
using FitFusion.Models;
using FitFusion.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TreinoController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public TreinoController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        [Authorize(Roles = Role.Treinador)]
        public async Task<IEnumerable<TreinoComExercicioDTO>> ListarTodosTreinos()
        {
            try
            {
                var treinos = await _contexto.Treinos.Include(t => t.Exercicios).ToListAsync();

                if (treinos == null)
                {
                    throw new Exception("Nenhum treino encontrado");
                }

                var treinosComExerciciosDTO = treinos
                    .Select(
                        treino =>
                            new TreinoComExercicioDTO
                            {
                                TreinoID = treino.TreinoID,
                                NomeTreino = treino.Nome,
                                DescricaoTreino = treino.Descricao,
                                DataCriacao = treino.DataCriacao
                            }
                    )
                    .ToList();

                return treinosComExerciciosDTO;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.Treinador)]
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
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<TreinoModel>> CriarNovoTreino(
            [FromBody] CriarTreino treinoDto
        )
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

        [HttpGet("{id}/exercicios")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<IEnumerable<ExerciciosModel>>> ObterExerciciosDoTreino(int id)
        {
            try
            {
                var treino = await _contexto.Treinos
                    .Include(t => t.Exercicios)
                    .FirstOrDefaultAsync(t => t.TreinoID == id);

                if (treino == null)
                {
                    return NotFound("Treino não encontrado");
                }

                var exerciciosDoTreino = treino.Exercicios.ToList();

                return exerciciosDoTreino;
            }
            catch (Exception)
            {
                // Trate exceções, se necessário
                throw;
            }
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<TreinoModel>> AtualizarTreino(
            TreinoModel treinoAtualizado,
            int id
        )
        {
            try
            {
                var treinoExistente = await _contexto.Treinos.FirstOrDefaultAsync(
                    t => t.TreinoID == id
                );

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
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<bool>> DeletaTreino(int id)
        {
            try
            {
                var treinoExistente = await _contexto.Treinos.FirstOrDefaultAsync(
                    t => t.TreinoID == id
                );

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
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult<IEnumerable<UsuarioTreinosDTO>>> ListarTreinosUsuario()
        {
            try
            {
                var usuarios = await _contexto.Usuarios
                    .Include(u => u.Treinos)
                    .ThenInclude(t => t.Exercicios)
                    .ToListAsync();

                var usuariosTreinosDTO = usuarios
                    .Select(
                        usuario =>
                            new UsuarioTreinosDTO
                            {
                                UserID = usuario.UserID,
                                NomeUsuario = usuario.Nome,
                                Treinos = usuario.Treinos
                                    .Select(
                                        treino =>
                                            new TreinoComExercicioDTO
                                            {
                                                TreinoID = treino.TreinoID,
                                                NomeTreino = treino.Nome,
                                                DescricaoTreino = treino.Descricao,
                                                Exercicios = treino.Exercicios.ToList()
                                            }
                                    )
                                    .ToList()
                            }
                    )
                    .ToList();

                return usuariosTreinosDTO;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("VincularTreinoUsuario")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult> VincularTreinoUsuario(
            [FromBody] VincularTreinoUsuarioDTO dadosVinculacao
        )
        {
            try
            {
                // Verifique se o usuário com o AspNetUserID especificado existe
                var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(
                    u => u.AspNetUserID == dadosVinculacao.AspNetUserID
                );

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Verifique se o treino com o TreinoID especificado existe
                var treino = await _contexto.Treinos.FirstOrDefaultAsync(
                    t => t.TreinoID == dadosVinculacao.TreinoID
                );

                if (treino == null)
                {
                    return NotFound("Treino não encontrado");
                }

                // Verifique se o usuário já possui o treino vinculado
                if (usuario.Treinos == null)
                {
                    usuario.Treinos = new List<TreinoModel>(); // Crie uma nova coleção, se for nula
                }

                if (usuario.Treinos.Any(t => t.TreinoID == dadosVinculacao.TreinoID))
                {
                    return BadRequest("Este treino já está vinculado ao usuário.");
                }

                // Vincule o treino ao usuário
                usuario.Treinos.Add(treino);

                await _contexto.SaveChangesAsync();

                return Ok("Treino vinculado ao usuário com sucesso.");
            }
            catch (System.Exception)
            {
                // Trate exceções, se necessário
                throw;
            }
        }

        [HttpDelete("DeletarTreinoUsuario")]
        [Authorize(Roles = Role.Treinador)]
        public async Task<ActionResult> DeletarTreinoUsuario([FromBody] DeletarTreinoUsuarioDTO dadosExclusao)
        {
            try
            {
                // Encontre o usuário com o AspNetUserID especificado
                var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == dadosExclusao.AspNetUserID);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Verifique se o treino com o TreinoID especificado está vinculado ao usuário
                var treino = usuario.Treinos.FirstOrDefault(t => t.TreinoID == dadosExclusao.TreinoID);

                if (treino == null)
                {
                    return NotFound("Treino não encontrado ou não vinculado ao usuário.");
                }

                // Desvincule o treino do usuário
                usuario.Treinos.Remove(treino);

                await _contexto.SaveChangesAsync();

                return Ok("Treino desvinculado do usuário com sucesso.");
            }
            catch (System.Exception)
            {
                // Trate exceções, se necessário
                throw;
            }
        }

    }
}
