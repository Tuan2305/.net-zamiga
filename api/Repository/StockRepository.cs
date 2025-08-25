using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.DTOs.Stock;
namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
                return null;

            // Update properties
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            _context.Stocks.Update(existingStock);
            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var stockToDelete = await _context.Stocks.FindAsync(id);

            if (stockToDelete == null)
                return false;

            _context.Stocks.Remove(stockToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
        
        public async Task<Stock?> GetBySymbolAsync(string symbol)
{
    return await _context.Stocks
        .FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
}
    }
}