using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaxisController : Controller
    {
        private readonly DataContext _context;

        public TaxisController(DataContext context)
        {
            _context = context;
        }

        // GET: Taxis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taxis.ToListAsync());
        }

        // GET: Taxis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaxiEntity taxiEntity = await _context.Taxis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxiEntity == null)
            {
                return NotFound();
            }

            return View(taxiEntity);
        }

        // GET: Taxis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taxis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //#REM se borra el bindeo para el dia de manhana si agregamos otros campos no agregarlos uno x uno
        public async Task<IActionResult> Create(TaxiEntity taxiEntity)
        {
            if (ModelState.IsValid)
            {
                taxiEntity.Plaque = taxiEntity.Plaque.ToUpper();
                _context.Add(taxiEntity);


                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already there is a record with the same plaque.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }

                }
            }
            return View(taxiEntity);
        }

        // GET: Taxis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaxiEntity taxiEntity = await _context.Taxis.FindAsync(id);
            if (taxiEntity == null)
            {
                return NotFound();
            }
            return View(taxiEntity);
        }

        // POST: Taxis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaxiEntity model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                model.Plaque = model.Plaque.ToUpper();
                _context.Update(model);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already there is a record with the same plaque.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(model);
        }


        // GET: Taxis/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var taxiEntity = await _context.Taxis
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (taxiEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(taxiEntity);
        //}

        // POST: Taxis/Delete/5
        //  [HttpPost, ActionName("Delete")]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            TaxiEntity taxiEntity = await _context.Taxis.FindAsync(id);
            _context.Taxis.Remove(taxiEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxiEntityExists(int id)
        {
            return _context.Taxis.Any(e => e.Id == id);
        }
    }
}
