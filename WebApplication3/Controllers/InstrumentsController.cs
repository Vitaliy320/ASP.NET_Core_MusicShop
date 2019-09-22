
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly SchoolContext _context;

        public InstrumentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Instruments
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CountrySortParm"] = sortOrder == "Country" ? "country_desc" : "Country";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            ViewData["CurrentFilter"] = searchString;

            var instruments = from s in _context.Instrument
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                instruments = instruments.Where(s => s.Name.Contains(searchString)
                || s.Country.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "country_desc":
                    instruments = instruments.OrderByDescending(s => s.Country);
                    break;
                case "Country":
                    instruments = instruments.OrderBy(s => s.Country);
                    break;
                case "price_desc":
                    instruments = instruments.OrderByDescending(s => s.Price);
                    break;
                case "Price":
                    instruments = instruments.OrderBy(s => s.Price);
                    break;
                case "name_desc":
                    instruments = instruments.OrderByDescending(s => s.Name);
                    break;

                default:
                    instruments = instruments.OrderBy(s => s.Name);
                    break;
            }
            return View(await instruments.AsNoTracking().ToListAsync());
        }

        // GET: Instruments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = await _context.Instrument.FirstOrDefaultAsync(m => m.ID == id);
            //var instrument = await _context.Instrument.Include(i => i.Customer).AsNoTracking().
              //  FirstOrDefaultAsync(m => m.ID == id);
                
            if (instrument == null)
            {
                return NotFound();
            }

            return View(instrument);
        }

        // GET: Instruments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Country,Price")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instrument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = await _context.Instrument.FindAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Country,Price")] Instrument instrument)
        {
            if (id != instrument.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instrument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstrumentExists(instrument.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = await _context.Instrument
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instrument == null)
            {
                return NotFound();
            }

            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instrument = await _context.Instrument.FindAsync(id);
            _context.Instrument.Remove(instrument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstrumentExists(int id)
        {
            return _context.Instrument.Any(e => e.ID == id);
        }
    }
}
