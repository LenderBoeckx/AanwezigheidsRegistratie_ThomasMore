using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boekingssysteem.Models
{
    public class Persoon
    {
        [Key]
        [Required(ErrorMessage = "Personeelnummer moet ingevuld zijn!")]
        [MinLength(8)]
        [MaxLength(8)]
        public string Personeelnummer { get; set; }
        [Required(ErrorMessage = "Naam moet ingevuld zijn!")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Voornaam moet ingevuld zijn!")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage = "Admin moet ingevuld zijn!")]
        public bool Admin { get; set; }
        public bool? Aanwezig { get; set; }
        [Required(ErrorMessage = "Wachtwoord moet ingevuld zijn!")]
        [MinLength(8)]
        public string Wachtwoord { get; set; }

        //Navigatieproperties
        public virtual ICollection<Afwezigheid> Afwezigheden { get; set; }
        public virtual ICollection<PersoonRichting> PersoonRichtingen { get; set; }
        public virtual ICollection<PersoonFunctie> PersoonFuncties { get; set; }

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}
