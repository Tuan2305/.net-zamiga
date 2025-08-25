using api.Models;
using api.Services;
namespace api.Interfaces
{
    public interface IFMPService
    {
        Task<Stock?> FindStockBySymbolAsync(string symbol);
    }
}