using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Boekingssysteem.Areas.Identity.Data;
using Boekingssysteem.Data;
using Boekingssysteem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Boekingssysteem.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly BoekingssysteemContext _context;

        public IndexModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager,
            BoekingssysteemContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Display(Name = "Email")]
        public string Username { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Naam")]
            [Required(ErrorMessage = "Naam moet ingevuld zijn")]
            public string Naam { get; set; }
            [Display(Name = "Voornaam")]
            [Required(ErrorMessage = "Voornaam moet ingevuld zijn")]
            public string Voornaam { get; set; }
        }

        private async Task LoadAsync(CustomUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var naam = await Task.FromResult(user.Naam);
            var voornaam = await Task.FromResult(user.Voornaam);

            Username = userName;
            Naam = naam;
            Voornaam = voornaam;

            Input = new InputModel
            {
                Naam = naam,
                Voornaam = voornaam
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.Naam = Input.Naam;
            user.Voornaam = Input.Voornaam;

            Persoon persoon = _context.Personen.Find(user.Personeelnummer);
            persoon.Naam = Input.Naam;
            persoon.Voornaam = Input.Voornaam;
            await _context.SaveChangesAsync();

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Uw profiel is aangepast";
            return RedirectToPage();
        }
    }
}
