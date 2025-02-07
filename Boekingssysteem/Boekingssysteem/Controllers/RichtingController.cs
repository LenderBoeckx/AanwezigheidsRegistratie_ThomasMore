using Boekingssysteem.Data;
using Boekingssysteem.Lib;
using Boekingssysteem.Models;
using Boekingssysteem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Boekingssysteem.Controllers
{
    [Authorize(Roles = "admin")]
    public class RichtingController : Controller
    {
        private readonly BoekingssysteemContext _context;

        public RichtingController(BoekingssysteemContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Toevoegen()
        {
            ViewBag.Visibility = "invisible";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toevoegen(RichtingCRUDViewModel viewModel)
        {
            var richtingen = _context.Richtingen.ToList();
            viewModel.Richtingen = richtingen;

            foreach (var functie in viewModel.Richtingen)
            {
                if (viewModel.Naam.ToLower() == functie.Naam.ToLower())
                {
                    ViewBag.Class = "alert alert-danger mb-5";
                    ViewBag.Visibility = "visible";

                    return View(viewModel);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(new Richting()
                    {
                        Naam = viewModel.Naam,
                    });

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Foutenlogboek.FoutLoggen(ex);
                }

            }
            return View(viewModel);
        }

        public IActionResult Overview()
        {
            RichtingCRUDViewModel viewModel = new RichtingCRUDViewModel();
            var lijstRichtingen = _context.Richtingen.ToList();
            viewModel.lijstRichtingen = lijstRichtingen;

            //ViewBag.Visibility = "invisible";
            return View(viewModel);
        }

        public async Task<IActionResult> Aanpassen(int? id)
        {
            if (id == null)
                return NotFound();

            Richting richting = _context.Richtingen.Where(d => d.RichtingID == id).FirstOrDefault();

            if (richting == null)
                return NotFound();

            RichtingCRUDViewModel vm = new RichtingCRUDViewModel()
            {
                RichtingID = richting.RichtingID,
                Naam = richting.Naam
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aanpassen(int id, RichtingCRUDViewModel viewModel)
        {
            if (id != viewModel.RichtingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Richting richting = new Richting()
                    {
                        RichtingID = viewModel.RichtingID,
                        Naam = viewModel.Naam
                    };
                    _context.Update(richting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!_context.Richtingen.Any(d => d.RichtingID == viewModel.RichtingID))
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
            return View(viewModel);
        }

        public async Task<IActionResult> Verwijderen(int id)
        {
            Richting richting = _context.Richtingen.Where(d => d.RichtingID == id).FirstOrDefault();
            if (richting != null)
            {
                RichtingCRUDViewModel vm = new RichtingCRUDViewModel()
                {
                    RichtingID = id,
                    Naam = richting.Naam
                };
            }

            _context.Richtingen.Remove(richting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Overview));
        }
    }
}