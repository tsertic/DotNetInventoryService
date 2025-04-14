using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
        {
            var items = await _context.InventoryItems.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetInventoryItem(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return Ok(inventoryItem);
        }

        // POST: api/Inventory

        [HttpPost]
        public async Task<ActionResult<InventoryItem>> PostInventoryItem(InventoryItem inventoryItem)
        {
            _context.InventoryItems.Add(inventoryItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventoryItem), new { id = inventoryItem.Id }, inventoryItem);

        }

        /*  [HttpPut("{id}")]
          public async Task<IActionResult> PutInventoryItem(int id,InventoryItem inventoryItem)
          {
              if(id!= inventoryItem.Id)
              {
                  return BadRequest("ID in URL does not match ID in body");
              }
              _context.Entry(inventoryItem).State = EntityState.Modified;
              try {
                  await _context.SaveChangesAsync();
              } catch (DbUpdateConcurrencyException) {
                  if (!InventoryItemExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }
                  return NoContent();
          }

  */


        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItem(int id,InventoryItem inventoryItem)
        {
            if (id != inventoryItem.Id)
            {
                return BadRequest("ID in url does not match id in body");
            }
            _context.Entry(inventoryItem).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (InventoryItemExists(id))
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
