using System;
using System.ComponentModel.DataAnnotations;

namespace Boekingssysteem.ViewModels
{
    public class CreateGebruikerViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Personeelnummer { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
