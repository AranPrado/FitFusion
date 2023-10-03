using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FitFusion.Database;
using FitFusion.DTOs;
using FitFusion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FitFusion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AutorizaController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly AppDbContext _contexto;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AutorizaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper,
            AppDbContext contexto,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _contexto = contexto;
            _configuration = configuration;
            _roleManager = roleManager;
        }


        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em : " + DateTime.Now.ToLongTimeString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioDTO model)
        {
            if (model.Senha != model.ConfirmarSenha)
            {
                return BadRequest("A senha e a confirmação de senha não correspondem.");
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                var usuarioModel = _mapper.Map<UsuarioModel>(model);

                var aspNetUserId = user.Id;
                usuarioModel.AspNetUserID = aspNetUserId;

                _contexto.Usuarios.Add(usuarioModel);
                await _contexto.SaveChangesAsync();

                await _signInManager.SignInAsync(user, false);
                return Ok(GerarToken(model));
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginInfo)
        {
            var resultado = await _signInManager.PasswordSignInAsync(loginInfo.Email,
                loginInfo.Senha, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                // Autenticação bem-sucedida; crie um UsuarioDTO com base no email
                var usuarioInfo = new UsuarioDTO { Email = loginInfo.Email };

                return Ok(GerarToken(usuarioInfo));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return BadRequest(ModelState);
            }
        }

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
                return Ok("Função criada com sucesso." + role);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }



        [HttpPost("roleUsuario/{userId}")]
        public async Task<IActionResult> AdicionarRoleAoUsuario(string userId, [FromBody] RoleIdDTO roleIdDto)
        {
            if (string.IsNullOrEmpty(userId) || roleIdDto == null || string.IsNullOrEmpty(roleIdDto.RoleID))
            {
                return BadRequest("O ID do usuário e o ID da função são obrigatórios.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var role = await _roleManager.FindByIdAsync(roleIdDto.RoleID);

            if (role == null)
            {
                return BadRequest("Função não encontrada.");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Ok("Função adicionada ao usuário com sucesso.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }








        private UsuarioToken GerarToken(UsuarioDTO usuarioInfo)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.UniqueName, usuarioInfo.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // Recupere as funções do usuário e adicione-as como reivindicações no token
            var user = _userManager.FindByEmailAsync(usuarioInfo.Email).Result;
            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais
            );

            return new UsuarioToken()
            {
                Autenticado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }


    }
}