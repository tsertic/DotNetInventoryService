using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks; 
namespace InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public InventoryController(InventoryDbContext context)
        {
            _context = context;
        }

        //GET AKCIJE
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetInventoryItems()
        {
            var itemsFromDb = await _context.InventoryItems.ToListAsync();

            var itemsDto = itemsFromDb.Select(item => new InventoryItemDto { 
                Id=item.Id,
                Name=item.Name,
                Quantity=item.Quantity,
                Price=item.Price
                
            });

            return Ok(itemsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItemDto>> GetInventoryItem(int id)
        {
            var itemFromDb = await _context.InventoryItems.FindAsync(id);
            if (itemFromDb == null)
            {
                return NotFound();
            }
            var itemDto = new InventoryItemDto{
                Id=itemFromDb.Id,
                Name=itemFromDb.Name,
                Quantity=itemFromDb.Quantity,
                Price=itemFromDb.Price  
            };
            return Ok(itemDto);
        }

        // POST: api/Inventory

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> PostInventoryItem(CreateInventoryItemDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newItem = new InventoryItem
            {
                Name = createDto.Name,
                Quantity = createDto.Quantity,
                Price = createDto.Price
            };
            _context.InventoryItems.Add(newItem);

            await _context.SaveChangesAsync();

            var createdItemDto = new InventoryItemDto
            {
                Id = newItem.Id,
                Name = newItem.Name,
                Quantity = newItem.Quantity,
                Price = newItem.Price
            };

            return CreatedAtAction(nameof(GetInventoryItem), new { id = createdItemDto.Id }, createdItemDto);

        }

     


        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItem(int id,UpdateInventoryItemDto updateDto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemFromDb = await _context.InventoryItems.FindAsync(id);
            if (itemFromDb == null)
            {
                return NotFound();
            }

            itemFromDb.Name = updateDto.Name;
            itemFromDb.Quantity = updateDto.Quantity;
            itemFromDb.Price = updateDto.Price;

            _context.Entry(itemFromDb).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!InventoryItemExists(id))
                {
                    return NotFound();
                }else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            _context.InventoryItems.Remove(inventoryItem);

            await _context.SaveChangesAsync();

            return NoContent();


        }
        //helper metode
        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItems.Any(e => e.Id == id);
        }

    }
}
