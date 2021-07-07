using ApiProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProj.Controllers
{
    [ApiController]
    [Route("main/Cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Get
            ([FromServices] ClienteContext context)
        {
            var clientes = await context.Clientes.AsNoTracking().ToListAsync();
            return clientes;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cliente>> Post
            ([FromServices] ClienteContext context,
            [FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
                return cliente;
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("{ddd}/{fone}")]
        public async Task<ActionResult<Cliente>> GetCliente(
            [FromServices] ClienteContext context, int ddd, string fone)
        {
            var cliente = await context.Clientes.Where(c => c.DDD == ddd && c.Fone == fone).FirstAsync();
            return cliente;
        }

        [HttpGet]
        [Route("{cnpj}")]
        public async Task<ActionResult<Cliente>> GetCliente(
            [FromServices] ClienteContext context, string cnpj)
        {
            var cliente = await context.Clientes.Where(c => c.CNPJouCpf == cnpj).FirstAsync();
            return cliente;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(
            [FromServices] ClienteContext context, int id)
        {
            var cliente = await context.Clientes.FindAsync(id);
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
            return cliente;
        }
    }
}
