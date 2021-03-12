using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza;
using BlazingPizza.Server;

namespace Pizza.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaSpecialsController : ControllerBase
    {
        private readonly PizzaStoreContext _context;

        public PizzaSpecialsController(PizzaStoreContext context)
        {
            _context = context;
        }

        // GET: api/PizzaSpecials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaSpecial>>> GetSpecials()
        {
            return await _context.Specials.ToListAsync();
        }

        // GET: api/PizzaSpecials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaSpecial>> GetPizzaSpecial(int id)
        {
            var pizzaSpecial = await _context.Specials.FindAsync(id);

            if (pizzaSpecial == null)
            {
                return NotFound();
            }

            return pizzaSpecial;
        }

        // PUT: api/PizzaSpecials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaSpecial(int id, PizzaSpecial pizzaSpecial)
        {
            if (id != pizzaSpecial.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizzaSpecial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaSpecialExists(id))
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

        // POST: api/PizzaSpecials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PizzaSpecial>> PostPizzaSpecial([FromBody]PizzaSpecial pizzaSpecial)
        {
            //_context.Specials.Add(pizzaSpecial);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPizzaSpecial", new { id = pizzaSpecial.Id }, pizzaSpecial);
            return Ok(new { Tai=pizzaSpecial});
        }

        // DELETE: api/PizzaSpecials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaSpecial(int id)
        {
            var pizzaSpecial = await _context.Specials.FindAsync(id);
            if (pizzaSpecial == null)
            {
                return NotFound();
            }

            _context.Specials.Remove(pizzaSpecial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaSpecialExists(int id)
        {
            return _context.Specials.Any(e => e.Id == id);
        }
    }
}
