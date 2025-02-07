using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Boekingssysteem.Areas.Identity.Data;
using Boekingssysteem.Data;
using Boekingssysteem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boekingssysteem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterAdminModel : PageModel
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly BoekingssysteemContext _context;

        public RegisterAdminModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            BoekingssysteemContext context)
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Naam moet ingevuld zijn")]
            [StringLength(100, ErrorMessage = "Naam moet ingevuld zijn")]
            [Display(Name = "Naam")]
            public string Naam { get; set; }

            [Required(ErrorMessage = "Voornaam moet ingevuld zijn")]
            [StringLength(100, ErrorMessage = "Voornaam moet ingevuld zijn")]
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }

            [Required(ErrorMessage = "Personeelnummer moet ingevuld zijn")]
            [StringLength(8, ErrorMessage = "Personeelnummer moet ingevuld zijn")]
            [MinLength(8, ErrorMessage = "Personeelnummer bestaat uit 8 tekens")]
            [MaxLength(8, ErrorMessage = "Personeelnummer bestaat uit 8 tekens")]
            [Display(Name = "Personeelnummer")]
            public string Personeelnummer { get; set; } 
            
            [Required(ErrorMessage = "Email moet ingevuld zijn")]
            [StringLength(100, ErrorMessage = "Email moet ingevuld zijn")]
            [EmailAddress(ErrorMessage = "Ongeldig email address")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Uw wachtwoord moet 8 karakters lang zijn")]
            [StringLength(100, ErrorMessage = "Uw wachtwoord moet 8 karakters lang zijn", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Herhaal wachtwoord")]
            [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {            
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {            
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var personen = _userManager.Users.ToList();
                Persoon persoon = _context.Personen.Find(Input.Personeelnummer);

                if (persoon == null)
                {
                    var user = new CustomUser { UserName = Input.Email, Email = Input.Email, Naam = Input.Naam, Voornaam = Input.Voornaam, Personeelnummer = Input.Personeelnummer };
                    if (user.Personeelnummer.Length != 8)
                    {
                        ModelState.AddModelError("", "Personeelnummer moet minstens 8 tekens bevatten");
                    }
                    if (user.Personeelnummer.Substring(0, 1).ToUpper() != "R" && user.Personeelnummer.Substring(0, 1).ToUpper() != "U" || 
                        !int.TryParse(user.Personeelnummer.Substring(1, 7), out int persnr))
                    {
                        ModelState.AddModelError("", "Personeelnummer bestaat uit R of U en 7 cijfers!");
                    }

                    else
                    {
                        var result = await _userManager.CreateAsync(user, Input.Password);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                                protocol: Request.Scheme);

                            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                            
                                _context.Add(new Persoon()
                                {
                                    Naam = Input.Naam,
                                    Voornaam = Input.Voornaam,
                                    Personeelnummer = Input.Personeelnummer,
                                    Admin = false,
                                    Wachtwoord = Input.Password
                                });
                                await _context.SaveChangesAsync();
                                
                                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                                {
                                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                                }
                                else
                                {
                                    //await _signInManager.SignInAsync(user, isPersistent: false);
                                    return LocalRedirect("~/Gebruiker/Index");
                                }                            
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }                                       
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Deze personeelsnummer is al ingebruik!");
                }                
            }

            // If we got this far, something failed, redisplay form
            //ModelState.Clear();
            return Page();
        }
    }
}
