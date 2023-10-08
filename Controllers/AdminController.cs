using FitFusion.Database;
using FitFusion.DTOs;
using FitFusion.DTOs.AdminsDTO;
using FitFusion.DTOs.TreinosDTO;
using FitFusion.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(AppDbContext contexto, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _contexto = contexto;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("RecuperarUsuarios")]
        public async Task<ActionResult<IEnumerable<RecuperarUsuariosDTO>>> RecuperarTodosUsuarios()
        {
            var usuarios = await _contexto.Usuarios.ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("RecuperarUsuarioID/{id}")]
        public async Task<ActionResult<RecuperarUsuariosDTO>> RecuperarUsuarioId(string id)
        {
            var usuarioID = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

            if (usuarioID == null)
            {
                return NotFound("Usuario não encontrado");
            }

            return Ok(usuarioID);
        }

        [HttpPut("AlterarUsuariosID/{id}")]
        public async Task<ActionResult<RecuperarUsuariosDTO>> AlterarUsuarioId([FromBody] RecuperarUsuariosDTO usuario, string id)
        {
            var usuarioID = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

            if (usuarioID == null) return NotFound("Usuario não encontrado");

            if (!string.IsNullOrEmpty(usuario.Nome)) usuarioID.Nome = usuario.Nome;

            if (!string.IsNullOrEmpty(usuario.Email)) usuarioID.Email = usuario.Email;

            if (usuario.Peso != null) usuarioID.Peso = usuario.Peso;

            if (usuario.Idade != null) usuarioID.Idade = usuario.Idade;

            if (usuario.Altura != null) usuarioID.Altura = usuario.Altura;

            await _contexto.SaveChangesAsync();

            return Ok(usuarioID);
        }

        [HttpDelete("DeletarUsuarioID/{id}")]
        public async Task<ActionResult<RecuperarUsuariosDTO>> DeletarUsuario(string id)
        {
            var usuarioID = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.AspNetUserID == id);

            if (usuarioID == null) return NotFound("Usuario não encontrado");

            _contexto.Usuarios.Remove(usuarioID);
            await _contexto.SaveChangesAsync();

            return Ok("Usuario deletado com sucesso");
        }

        /////////////////////////////////////////////////////////////////////////

        [HttpGet("RecuperarTreinos")]
        public async Task<ActionResult<IEnumerable<RecuperarTreinosDTO>>> RecuperarTodosTreinos()
        {
            var treinos = await _contexto.Treinos.ToListAsync();

            return Ok(treinos);
        }

        [HttpGet("RecuperarTreinoID/{id}")]

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

        [HttpPut("AtualizarTreino/{id}")]

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

        [HttpDelete("DeletaTreino/{id}")]

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

        /////////////////////////////////////////////////////////////////////////

        [HttpGet("RecuperarExercicios")]
        public async Task<ActionResult<IEnumerable<RecuperarExerciciosDTO>>> RecuperarTodosExercicios()
        {
            var exercicios = await _contexto.Exercicios.ToListAsync();

            return Ok(exercicios);
        }

        [HttpGet("RecuperarExercicioID/{id}")]
        
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

        [HttpPut("AtualizarExercicio/{id}")]
        
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



        [HttpDelete("DeletaExercicio/{id}")]
        
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

        /////////////////////////////////////////////////////////////////////////

        [HttpPost("CriarRoles")]
        public async Task<IActionResult> CriarRoles([FromBody] RoleDTO roleInfo)
        {
            if (roleInfo == null || string.IsNullOrEmpty(roleInfo.RoleName))
            {
                return BadRequest("Os atributos da função são inválidos.");
            }

            // Use os valores fornecidos para criar uma nova role
            var role = new IdentityRole
            {
                Name = roleInfo.RoleName,
                NormalizedName = roleInfo.RoleNormalizedName,
                ConcurrencyStamp = roleInfo.RoleConcurrencyStamp
            };

            var resultado = await _roleManager.CreateAsync(role);

            if (resultado.Succeeded)
            {
                return Ok("Função criada com sucesso. " + role.Id);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("roleUsuario/{userId}")]
        public async Task<IActionResult> AdicionarRoleAoUsuario(
            string userId,
            [FromBody] RoleIdDTO role
        )
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role.RoleNome))
            {
                return BadRequest("O ID do usuário e o nome da função são obrigatórios.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var roleName = role.RoleNome; // Obtenha o nome da função do DTO

            // Verifique se a função existe
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                return BadRequest("Função não encontrada.");
            }

            // Adicione o usuário à função
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok("Função adicionada ao usuário com sucesso.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}