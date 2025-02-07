using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Boekingssysteem.Areas.Identity.Data;
using Boekingssysteem.Models;
using Boekingssysteem.Data;

namespace Boekingssysteem.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly BoekingssysteemContext _context;

        public EmailModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager,
            IEmailSender emailSender,
            BoekingssysteemContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress]
            [Display(Name = "Nieuwe email")]
            [Required(ErrorMessage = "Email moet ingevuld zijn")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(CustomUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
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

        public async Task<IActionResult> OnPostChangeEmailAsync()
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

            var users = _userManager.Users.ToList();

            foreach (var gebruiker in users)
            {
                if (gebruiker.Email == Input.NewEmail)
                {
                    ModelState.AddModelError(string.Empty, "Dit emailadres is al in gebruik!");
                    //StatusMessage = "Dit emailadres is al in gebruik!";
                    return Page();
                }
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                //var userId = await _userManager.GetUserIdAsync(user);
                //var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmailChange",
                //    pageHandler: null,
                //    values: new { userId = userId, email = Input.NewEmail, code = code },
                //    protocol: Request.Scheme);
                //await _emailSender.SendEmailAsync(
                //    Input.NewEmail,
                //    "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                user.UserName = Input.NewEmail;
                user.Email = Input.NewEmail;
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, isPersistent: false);
                // Moesten we email aan Persoon toevoegen staat dit al klaar
                //Persoon persoon = _context.Personen.Find(user.Personeelnummer);
                //persoon.Email = email;
                //await _context.SaveChangesAsync();

                StatusMessage = "Uw email is aangepast";
                return RedirectToPage();
            }

            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var users = _userManager.Users.ToList();

            foreach (var gebruiker in users)
            {
                if (gebruiker.Email == Input.NewEmail)
                {
                    ModelState.AddModelError(string.Empty, "Dit emailadres is al in gebruik!");
                }
            }
            
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

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var result = await _userManager.ConfirmEmailAsync(user, code);
            //var callbackUrl = Url.Page(
            //    "/Account/ConfirmEmail",
            //    pageHandler: null,
            //    values: new { area = "Identity", userId = userId, code = code },
            //    protocol: Request.Scheme);
            //await _emailSender.SendEmailAsync(
            //    email,
            //    "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
