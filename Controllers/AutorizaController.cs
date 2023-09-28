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

        public AutorizaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper,
            AppDbContext contexto,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _contexto = contexto;
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em : " + DateTime.Now.ToLongTimeString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioDTO model)
        {
            // if(!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            // }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
            };

            var resulto = await _userManager.CreateAsync(user, model.Senha);

            if (resulto.Succeeded)
            {
                var usuarioModel = _mapper.Map<UsuarioModel>(model);

                _contexto.Usuarios.Add(usuarioModel);
                await _contexto.SaveChangesAsync();

                await _signInManager.SignInAsync(user, false);
                return Ok(GerarToken(model));
            }
            else
            {
                return BadRequest(resulto.Errors);
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


        private UsuarioToken GerarToken(UsuarioDTO usuarioInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioInfo.Email),
                new Claim("teste", "Aran"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

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