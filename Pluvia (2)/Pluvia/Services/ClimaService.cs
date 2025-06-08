using System.Net.Http;
using System.Threading.Tasks;

namespace Pluvia.Services;

public class ClimaService
{
    private readonly HttpClient _httpClient;

    public ClimaService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> ObterClimaAsync(double latitude, double longitude)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,weathercode";

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"{{ \"erro\": \"Falha ao obter clima: {ex.Message}\" }}";
        }
    }
}
