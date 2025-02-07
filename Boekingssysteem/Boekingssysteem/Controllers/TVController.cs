using Boekingssysteem.Data;
using Boekingssysteem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boekingssysteem.Controllers
{
    public class TVController : Controller
    {
        private readonly BoekingssysteemContext _context;

        public TVController(BoekingssysteemContext context)
        {
            _context = context;
        }     

        public async Task <IActionResult> Index()
        {
            var personen = _context.Personen.ToList();           
            var persoonrichtingen = _context.PersoonRichtingen.Include(x => x.Persoon).Include(z => z.Richting);
            var persoonfuncties = _context.PersoonFuncties.Include(x => x.Persoon).Include(z => z.Functie);
            var richtingen = _context.Richtingen;
            var afwezigheden = _context.Afwezigheden;

            PersoonListViewModel plvm = new PersoonListViewModel();
            plvm.Personen = personen;
            plvm.PersoonRichtingen = persoonrichtingen.ToList();
            plvm.PersoonFuncties = persoonfuncties.ToList();
            plvm.Richtingen = richtingen.ToList();
            plvm.Afwezigheden = afwezigheden.ToList();
          
            return View(plvm);
        }

    }
}
