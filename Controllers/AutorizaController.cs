using FitFusion.Database;
using FitFusion.DTOs;
using FitFusion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitFusion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AutorizaController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public AutorizaController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _singInManager = signInManager;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em : " + DateTime.Now.ToLongTimeString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioModel model)
        {
            // if(!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            // }

            var user = new IdentityUser
            {
                UserName = model.Nome,
                Email = model.Email,
                EmailConfirmed = true,
            };

            var resulto = await _userManager.CreateAsync(user, model.Senha);

            if (resulto.Succeeded)
            {
                var usuarioModel = new UsuarioModel
                {
                    Nome = model.Nome,
                    Peso = model.Peso,
                    Idade = model.Idade,
                    Altura = model.Altura
                };

                _contexto.Usuarios.Add(usuarioModel);
                await _contexto.SaveChangesAsync();

                await _singInManager.SignInAsync(user, false);
                return Ok();
            } 
            else
            {
                return BadRequest(resulto.Errors);
            }


        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UsuarioModel usuarioInfo)
        {
            var resultado = await _singInManager.PasswordSignInAsync(usuarioInfo.Email,
            usuarioInfo.Senha, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Invalido....");
                return BadRequest(ModelState);
            }
        }
    }
}