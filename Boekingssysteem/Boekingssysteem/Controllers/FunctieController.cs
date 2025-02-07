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
    public class FunctieController : Controller
    {
        private readonly BoekingssysteemContext _context;

        public FunctieController(BoekingssysteemContext context)
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
        public async Task<IActionResult> Toevoegen(FunctieCRUDViewModel viewModel)
        {
            var functies = _context.Functies.ToList();
            viewModel.Functies = functies;

            foreach (var functie in viewModel.Functies)
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
                    _context.Add(new Functie()
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
            FunctieCRUDViewModel viewModel = new FunctieCRUDViewModel();
            var lijstFuncties = _context.Functies.ToList();
            viewModel.lijstFuncties = lijstFuncties;

            //ViewBag.Visibility = "invisible";
            return View(viewModel);
        }

        public async Task<IActionResult> Aanpassen(int? id) 
        {
            if (id == null)
                return NotFound();

            Functie functie = _context.Functies.Where(d => d.FunctieID == id).FirstOrDefault();

            if (functie == null)
                return NotFound();

            FunctieCRUDViewModel vm = new FunctieCRUDViewModel()
            {
                FunctieID = functie.FunctieID,
                Naam = functie.Naam
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aanpassen(int id, FunctieCRUDViewModel viewModel)
        {
            if (id != viewModel.FunctieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Functie functie = new Functie()
                    {
                        FunctieID = viewModel.FunctieID,
                        Naam = viewModel.Naam
                    };
                    _context.Update(functie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!_context.Functies.Any(d => d.FunctieID == viewModel.FunctieID))
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
            Functie functie = _context.Functies.Where(d => d.FunctieID == id).FirstOrDefault();
            if (functie != null)
            {
                FunctieCRUDViewModel vm = new FunctieCRUDViewModel()
                {
                    FunctieID = id,
                    Naam = functie.Naam
                };
            }

            _context.Functies.Remove(functie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Overview));
        }
    }
}