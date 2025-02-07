using Boekingssysteem.Data;
using Boekingssysteem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Boekingssysteem.Models;
using Boekingssysteem.Lib;
using System;
using static Boekingssysteem.Areas.Identity.Pages.Account.ExternalLoginModel;
using System.ComponentModel.DataAnnotations;
using Boekingssysteem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace Boekingssysteem.Controllers
{
    public class StatusController : Controller
    {
        private readonly BoekingssysteemContext _context;
        private readonly ILogger<StatusController> _logger;
        private UserManager<CustomUser> _userManager;

        public StatusController(ILogger<StatusController> logger, BoekingssysteemContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {         
            [Display(Name = "Personeelnummer moet ingevuld zijn")]
            public string Personeelnummer { get; set; }
        }

        public ViewResult Index()
        {            
            var personen = _context.Personen.ToList();
            var persoonrichtingen = _context.PersoonRichtingen.Include(x => x.Persoon).Include(z => z.Richting);
            var persoonfuncties = _context.PersoonFuncties.Include(x => x.Persoon).Include(z => z.Functie);
            PersoonListViewModel plvm = new PersoonListViewModel();
            plvm.Personen = personen;
            plvm.PersoonRichtingen = persoonrichtingen.ToList();
            plvm.PersoonFuncties = persoonfuncties.ToList();

            return View(plvm);
        }

        public IActionResult StatusAdmin()
        {
            var personen = _context.Personen.ToList();
            PersoonCRUDViewModel plvm = new PersoonCRUDViewModel();
            plvm.Personen = personen;

            return View(plvm);
        }

        public async Task<IActionResult> StatusAanpassen(PersoonCRUDViewModel vm)
        {
            string personeelsnummer = vm.Personeelnummer;

            if (personeelsnummer == null)
            {
                return NotFound();
            }

            var persoon = await _context.Personen.FindAsync(personeelsnummer);
            if (persoon == null)
            {
                return NotFound();
            }

            if (persoon.Aanwezig == null)
            {
                PersoonCRUDViewModel viewModel = new PersoonCRUDViewModel()
                {
                    Aanwezig = true
                };
                await _context.SaveChangesAsync();
                return View(viewModel);
            }
            else if (persoon.Aanwezig == true)
            {
                PersoonCRUDViewModel viewModel = new PersoonCRUDViewModel()
                {
                    Aanwezig = false
                };
                await _context.SaveChangesAsync();
                return View(viewModel);
            }
            else
            {
                PersoonCRUDViewModel viewModel = new PersoonCRUDViewModel()
                {
                    Aanwezig = true
                };
                await _context.SaveChangesAsync();
                return View(viewModel);
            }

            return View();
        }

        public IActionResult StatusData(string personeelnummer)
        {
            var personen = _context.Personen.ToList();
            PersoonCRUDViewModel plvm = new PersoonCRUDViewModel();
            plvm.Personen = personen;

            try
            {
                Persoon persoon = _context.Personen.Find(personeelnummer);
                if (persoon == null)
                {
                    ModelState.AddModelError(string.Empty, "Het personeelnummer is onjuist");
                }
                else
                {
                    ViewBag.naam = persoon.Naam;
                    ViewBag.voornaam = persoon.Voornaam;
                    ViewBag.personeelnummer = persoon.Personeelnummer;
                    ViewBag.Aanwezig = persoon.Aanwezig;                    
                }

                return View("StatusAdmin", plvm);
            }
            catch (Exception e)
            {

                Foutenlogboek.FoutLoggen(e);
            }
            return View();
        }

        [HttpPost, ActionName("Aanpassen")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aanpassen(string personeelnummer, PersoonCRUDViewModel viewModel)
        {
            if (personeelnummer == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    Persoon persoon = _context.Personen.Find(personeelnummer);
                    if (persoon.Aanwezig == true)
                    {
                        persoon.Aanwezig = false;

                        _context.Update(persoon);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        persoon.Aanwezig = true;
                        _context.Update(persoon);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Personen.Any(e => e.Personeelnummer == viewModel.Personeelnummer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            ViewBag.JavaScriptFunction = string.Format("MeldingSucces('{0}');", personeelnummer);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AanpassenViaId")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AanpassenViaId(string personeelnummer, PersoonCRUDViewModel viewModel)
        {            
            if (personeelnummer == null)
            {
                ModelState.AddModelError(string.Empty, "Het personeelnummer is onjuist");
            }
            else
            {
                try
                {
                    Persoon persoon = _context.Personen.Find(personeelnummer);                    
                    if (persoon.Aanwezig == true)
                    {
                        persoon.Aanwezig = false;

                        _context.Update(persoon);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        persoon.Aanwezig = true;
                        _context.Update(persoon);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Personen.Any(e => e.Personeelnummer == viewModel.Personeelnummer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }                           
                ViewBag.JavaScriptFunction = string.Format("MeldingSucces('{0}');", personeelnummer);
                return RedirectToAction("StatusAdmin");           
        }

        [HttpGet]
        public IActionResult StatusIndividueel()
        {
            CustomUser user = _userManager.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            Persoon persoon = _context.Personen.Where(p => p.Personeelnummer == user.Personeelnummer).FirstOrDefault();

            PersoonCRUDViewModel viewModel = new PersoonCRUDViewModel()
            {
                Voornaam = persoon.Voornaam,
                Naam = persoon.Naam,
                Personeelnummer = persoon.Personeelnummer,
                Aanwezig = persoon.Aanwezig
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StatusIndividueel(PersoonCRUDViewModel viewModel) 
        {
            CustomUser user = _userManager.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            Persoon persoon = _context.Personen.Where(p => p.Personeelnummer == user.Personeelnummer).FirstOrDefault();

            if (persoon.Aanwezig == true)
            {
                persoon.Aanwezig = false;
            }
            else
            {
                persoon.Aanwezig = true;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(StatusIndividueel));
        }
    }
}
