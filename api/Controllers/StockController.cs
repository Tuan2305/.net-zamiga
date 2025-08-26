using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Helpers;

namespace api.Controllers


{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);  
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDtos = stocks.Select(stock => stock.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDTO();

            // Add to database using context instead of repository
            await _context.Stocks.AddAsync(stockModel); // await : su dung bat dong bo
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the existing stock
            var stockToUpdate = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            // If not found, return 404
            if (stockToUpdate == null)
            {
                return NotFound($"Stock with ID {id} not found");
            }

            // Update the stock properties
            updateDto.ToStockFromUpdateDTO(stockToUpdate);

            // Set the EntityState to Modified
            _context.Stocks.Update(stockToUpdate);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated stock as DTO
            return Ok(stockToUpdate.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the stock
            var stockToDelete = await _context.Stocks.FindAsync(id);
            
            // If not found, return 404
            if (stockToDelete == null)
            {
                return NotFound($"Stock with ID {id} not found");
            }

            // Remove the stock
            _context.Stocks.Remove(stockToDelete);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            
            // Return 204 No Content
            return NoContent();
        }
    }
}