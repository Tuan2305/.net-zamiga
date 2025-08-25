using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        // Map from Entity to DTO
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                // CommentsCount is no longer included
            };
        }

        // Map from List of Entities to List of DTOs
        public static IEnumerable<StockDto> ToStockDtoList(this IEnumerable<Stock> stocks)
        {
            return stocks.Select(stock => stock.ToStockDto());
        }

        // Map from DTO to Entity (useful for create/update operations)
        public static Stock ToStockModel(this StockDto stockDto)
        {
            return new Stock
            {
                Id = stockDto.Id,
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
                // Note: We don't map Comments or Portfolios here
                // since those would typically be managed separately
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
        
        public static Stock ToStockFromUpdateDTO(this UpdateStockRequestDto updateDto, Stock existingStock)
        {
            existingStock.Symbol = updateDto.Symbol;
            existingStock.CompanyName = updateDto.CompanyName;
            existingStock.Purchase = updateDto.Purchase;
            existingStock.LastDiv = updateDto.LastDiv;
            existingStock.Industry = updateDto.Industry;
            existingStock.MarketCap = updateDto.MarketCap;
            
            return existingStock;
        }

    }
}