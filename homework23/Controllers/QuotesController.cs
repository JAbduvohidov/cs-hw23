using System;
using System.Threading.Tasks;
using homework23.DataContext;
using homework23.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homework23.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Quote model)
        {
            if (model == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            model.InsertDate = DateTime.Now;

            await _quotesDbContext.Quotes.AddAsync(model);
            await _quotesDbContext.SaveChangesAsync();
            return Created("Quote created", model);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll() => Ok(await _quotesDbContext.Quotes.ToListAsync());

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            var quote = await _quotesDbContext.Quotes.FindAsync(id);
            if (quote == null) return NotFound();

            return Ok(quote);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Quote model)
        {
            if (id <= 0) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var quote = await _quotesDbContext.Quotes.FindAsync(id);

            if (quote == null) return NotFound();

            quote.Author = model.Author;
            quote.Text = model.Text;

            if (await _quotesDbContext.SaveChangesAsync() == 0) return BadRequest("edit customer failed");
            return Ok(quote);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            var quote = await _quotesDbContext.Quotes.FindAsync(id);

            _quotesDbContext.Quotes.Remove(quote);
            await _quotesDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}