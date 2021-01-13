using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArduinoSystem.Data;
using ArduinoSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace ArduinoSystem.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly ArduinoSystemContext _context;
        private readonly UserManager<ApplicationUser> _usrMgr;


        public ChannelsController(UserManager<ApplicationUser> userManager, ArduinoSystemContext context)
        {
            _context = context;
            _usrMgr = userManager;
        }

        public async Task<IActionResult> Index(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                var loggedUser = _usrMgr.GetUserAsync(HttpContext.User);
                var arduinoSystemContext =
                    _context.Channels
                    .Include(c => c.User)
                    .Where(c => c.UserId == loggedUser.Result.Id);

                return View(await arduinoSystemContext.ToListAsync());
            }
            else
            {
                var user = await _context.Users.FindAsync(userId);
                ViewBag.User = user;
                var arduinoSystemContext =
                    _context.Channels
                    .Include(c => c.User)
                    .Where(c => c.UserId == userId);

                return View(await arduinoSystemContext.ToListAsync());
            }
        }

        [HttpGet]
        public IActionResult View(Guid apikey, int field, int? take)
        {
            var model = new GraphicViewModel();

            var values =
                
                _context.Entries
                .Where(x => x.ChannelId == apikey)
                .OrderByDescending(x => x.Id)
                .Take(take == null ? 20 : take.Value)
                .OrderBy(x => x.Id);

            if (values.Count() > 0)
            {

                var xValues = values.Select(x => x.CreatedAt.ToString(System.Globalization.CultureInfo.InvariantCulture));
                IEnumerable<string> yValues = values.Select(x => x.Field1.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                switch (field)
                {
                    case 1:
                        yValues = values.Select(x => x.Field1.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field1_Name;
                        break;
                    case 2:
                        yValues = values.Select(x => x.Field2.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field2_Name;
                        break;
                    case 3:
                        yValues = values.Select(x => x.Field3.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field3_Name;
                        break;
                    case 4:
                        yValues = values.Select(x => x.Field4.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field4_Name;
                        break;
                    case 5:
                        yValues = values.Select(x => x.Field5.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field5_Name;
                        break;
                    case 6:
                        yValues = values.Select(x => x.Field6.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field6_Name;
                        break;
                    case 7:
                        yValues = values.Select(x => x.Field7.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field7_Name;
                        break;
                    case 8:
                        yValues = values.Select(x => x.Field8.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        model.graphName = _context.Channels.First(x => x.Id == apikey).Field8_Name;
                        break;
                }

                model.xValues = $"[\"{String.Join("\", \"", xValues)}\"]";
                model.yValues = $"[\"{String.Join("\", \"", yValues)}\"]";
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Field1_Name,Field2_Name,Field3_Name,Field4_Name,Field5_Name,Field6_Name,Field7_Name,Field8_Name")] Channel channel)
        {
            var user = _usrMgr.GetUserAsync(User).Result;
            if (ModelState.IsValid)
            {
                channel.UserId = user.Id;
                _context.Add(channel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

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
