using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArduinoSystem.Data;
using ArduinoSystem.Models;

namespace ArduinoSystem.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly ArduinoSystemContext _context;

        public ChannelsController(ArduinoSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index([Bind(Prefix = "userId")]string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.User = user;
            var arduinoSystemContext =
                _context.Channels
                .Include(c => c.User)
                .Where(c => c.UserId == userId);

            return View(await arduinoSystemContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult View(Guid apikey, int field, int take)
        {
            var model = new GraphicViewModel();

            var values = 
                _context.Entries
                .Where(x => x.ChannelId == apikey)
                .Take(take)
                .OrderByDescending(x => x.CreatedAt);

            var xValues = values.Select(x => x.CreatedAt.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var yValues = values.Select(x => x.Field1.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));

            model.xValues = $"[\"{String.Join("\", \"", xValues)}\"]";
            model.yValues = $"[\"{String.Join("\", \"", yValues)}\"]";

            model.graphName = _context.Channels.First(x => x.Id == apikey).Field1_Name;

            return View(model);
        }

        public IActionResult Create([Bind(Prefix = "accountid")]Guid accountId)
        {
            var account = _context.Users.Find(accountId);
            ViewData["Account"] = account;
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId,Field1_Name,Field2_Name,Field3_Name,Field4_Name,Field5_Name,Field6_Name,Field7_Name,Field8_Name")] Channel channel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(channel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { accountid = channel.UserId });
            }
            var account = await _context.Users.FindAsync(channel.UserId);
            ViewData["Account"] = account;
            return View(channel);
        }

        // GET: Channels/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }
            return View(channel);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,AccountId,Field1_Name,Field2_Name,Field3_Name,Field4_Name,Field5_Name,Field6_Name,Field7_Name,Field8_Name")] Channel channel)
        {
            if (id != channel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(channel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelExists(channel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { accountid = channel.UserId });
            }

            return View(channel);
        }

        // GET: Channels/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels
                .Include(c => c.UserId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            return View(channel);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var channel = await _context.Channels.FindAsync(id);
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { accountid = channel.UserId });
        }

        private bool ChannelExists(Guid id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }
    }
}
