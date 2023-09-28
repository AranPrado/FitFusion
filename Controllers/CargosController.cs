using FitFusion.Database;
using FitFusion.Models;
using FitFusion.Repositores.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CargosController : ICargosRepositore
    {

        private readonly AppDbContext _contexto;

        public CargosController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IEnumerable<CargoModel>> ListarTodosCargos()
        {
            try
            {
                return await _contexto.Cargos.ToListAsync();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CargoModel>> ProcurarCargoPorId(int id)
        {
            try
            {
                var cargoExistente = await _contexto.Cargos.FirstOrDefaultAsync(c => c.CargoID == id);

                if (cargoExistente == null)
                {
                    return new NotFoundObjectResult("Cargo não encontrado");
                }

                return cargoExistente;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("CriarCargo")]
        public async Task<CargoModel> CriarNovoCargo(CargoModel cargoModel)
        {
        try
        {
                await _contexto.Cargos.AddAsync(cargoModel);
                await _contexto.SaveChangesAsync();
    
                return cargoModel;
        }
        catch (System.Exception)
        {
            
            throw;
        }
        }

        [HttpPut("atualizar/{id}")]
        public Task<ActionResult<CargoModel>> AtualizarCargo(CargoModel cargoModel, int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult<bool>> DeletarCargo(int id)
        {
           try
           {
             var cargoExistente = await _contexto.Cargos.FirstOrDefaultAsync(c => c.CargoID == id);
 
                 if (cargoExistente == null)
                 {
                     return new NotFoundObjectResult("Cargo não encontrado");
                 }
 
             _contexto.Cargos.Remove(cargoExistente);
             await _contexto.SaveChangesAsync();
             return true;
           }
           catch (System.Exception)
           {
            
            throw;
           }
        }

        [HttpGet("Usuarios")]
        public async Task<IEnumerable<UsuarioModel>> ListarCargosUsuarios()
        {
            try
            {
                return await _contexto.Usuarios.Include(c => c.Cargo).ToListAsync();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }




    }
}