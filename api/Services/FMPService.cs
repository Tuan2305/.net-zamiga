using System.Text.Json;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        
        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        
        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                string apiKey = _config["FMPKey"] ?? throw new ArgumentNullException("FMP API key not found");
                string url = $"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={apiKey}";
                
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return null;
                    
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<Stock>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                return result?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}