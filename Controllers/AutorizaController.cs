using AutoMapper;
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
        private readonly IMapper _mapper;

        private readonly AppDbContext _contexto;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AutorizaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper,
            AppDbContext contexto)
                {
                    _userManager = userManager;
                    _signInManager = signInManager;
                    _mapper = mapper;
                    _contexto = contexto;
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
                return Ok();
            }
            else
            {
                return BadRequest(resulto.Errors);
            }


        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO usuarioInfo)
        {
            var resultado = await _signInManager.PasswordSignInAsync(usuarioInfo.Email,
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