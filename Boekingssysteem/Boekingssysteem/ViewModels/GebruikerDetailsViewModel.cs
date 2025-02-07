using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Boekingssysteem.ViewModels
{
    public class GebruikerDetailsViewModel
    {
        public string Id { get; set; }
        public string Naam { get; set; }
        public string UserName { get; set; }
        public string Voornaam { get; set; }
        public string Personeelnummer { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}
