using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pluvia.Data;
using Pluvia.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Pluvia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Endereco) // inclui endereço do usuário
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Cpf,
                Endereco = new
                {
                    usuario.Endereco.Id,
                    usuario.Endereco.Cidade,
                    usuario.Endereco.Bairro,
                    usuario.Endereco.Cep,
                    usuario.Endereco.Estado,
                    usuario.Endereco.Logradouro,
                    usuario.Endereco.Latitude,
                    usuario.Endereco.Longitude
                }
            });
        }

        // GET: api/usuarios/{id}/climas
        [HttpGet("{id}/climas")]
        public async Task<IActionResult> GetClimasPorUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            var climas = await _context.Climas
                .Where(c => c.EnderecoId == usuario.EnderecoId)
                .OrderByDescending(c => c.DataHorario)
                .ToListAsync();

            return Ok(climas);
        }

        // GET: api/usuarios/{id}/alertas
        [HttpGet("{id}/alertas")]
        public async Task<IActionResult> GetAlertasPorUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            var alertas = await _context.Alertas
                .Where(a => a.UsuarioId == id)
                .OrderByDescending(a => a.DataHorario)
                .ToListAsync();

            return Ok(alertas);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest(new { mensagem = "Id do usuário não corresponde." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                    return NotFound(new { mensagem = "Usuário não encontrado." });
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(u => u.Id == id);
        }
    }
}
