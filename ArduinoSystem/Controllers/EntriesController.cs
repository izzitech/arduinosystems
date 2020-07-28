using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArduinoSystem.Data;
using System.IO;
using System.Text;
using System.Collections.Specialized;

namespace ArduinoSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly ArduinoSystemContext _context;

        public EntriesController(ArduinoSystemContext context)
        {
            _context = context;
        }

        // GET: api/Entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        // PUT: api/Entries/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntry(int id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        // POST: api/Entries
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /*
        [HttpPost]
        public async Task<ActionResult<Entry>> PostEntry(Entry entry)
        {
            _context.Entries.Add(entry);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetEntry", new { id = entry.Id }, entry);
        }
        */

        [HttpPost]
        public async Task<Entry> PostEntry()
        {
            Guid channelId;
            if(Guid.TryParse(Request.Headers["X-APIKEY"], out channelId))
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    var parametersDictionary = System.Web.HttpUtility.ParseQueryString(body);

                    Entry entry = new Entry();
                    entry.ChannelId = channelId;

                    foreach(var property in typeof(Entry).GetProperties())
                    {
                        Double value;
                        if (Double.TryParse(parametersDictionary[property.Name],System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value)){
                            entry
                                .GetType()
                                .GetProperty(property.Name)
                                .GetSetMethod()
                                .Invoke(entry, new object[] { value });
                        }
                    }
                    
                    entry.CreatedAt = DateTime.Now;

                    _context.Entries.Add(entry);
                    await _context.SaveChangesAsync();
                    return entry;
                }
            }
            return null;
        }

        private Double TryParseValue(string input)
        {

            return 1.0;
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Entry>> DeleteEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return entry;
        }

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.Id == id);
        }
    }
}
