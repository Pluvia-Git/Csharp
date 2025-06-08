using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pluvia.Models; // ajuste conforme o namespace do seu projeto
using Pluvia.Data;   // ajuste conforme o namespace do seu DbContext

namespace Pluvia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClimaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "12c1487aa8317768af0265b6ca00854e";

        public ClimaController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("buscar-e-salvar/{idUsuario}")]
        public async Task<IActionResult> BuscarEInserirClimaPorUsuario(int idUsuario)
        {
            // 1. Buscar o usuário com o endereço
            var usuario = await _context.Usuarios
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(u => u.Id == idUsuario);

            if (usuario == null || usuario.Endereco == null)
                return NotFound("Usuário ou endereço não encontrado.");

            var latitude = usuario.Endereco.Latitude;
            var longitude = usuario.Endereco.Longitude;

            // 2. Buscar clima na API externa
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric&lang=pt";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Erro ao buscar clima na API externa.");

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonDocument.Parse(content).RootElement;

            // 3. Criar objeto Clima
            var clima = new Clima
            {
                EnderecoId = usuario.Endereco.Id,
                DataHorario = DateTime.UtcNow,
                Condicao = data.GetProperty("weather")[0].GetProperty("main").GetString(),
                Descricao = data.GetProperty("weather")[0].GetProperty("description").GetString(),
                Temperatura = (float)data.GetProperty("main").GetProperty("temp").GetDecimal(),
                Pressao = (float)data.GetProperty("main").GetProperty("pressure").GetDecimal(),
                Umidade = (float)data.GetProperty("main").GetProperty("humidity").GetDecimal(),
                VelocidadeVento = (float)data.GetProperty("wind").GetProperty("speed").GetDecimal(),
                Nuvens = (float)data.GetProperty("clouds").GetProperty("all").GetDecimal(),
                Esp32 = 0f
            };

            // 4. Salvar no banco
            _context.Climas.Add(clima);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Clima armazenado com sucesso.", clima });
        }
    }
}
