using System.ComponentModel.DataAnnotations;
using System;

namespace Boekingssysteem.Models
{
    public class Afwezigheid
    {
        public int AfwezigheidID { get; set; }

        [Required(ErrorMessage = "Personeelnummer moet ingevuld zijn!")]
        [MinLength(8)]
        [MaxLength(8)]
        public string Personeelnummer { get; set; }

        [Required(ErrorMessage = "Begindatum moet ingevuld zijn!")]
        [DataType(DataType.Date)]
        public DateTime Begindatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Einddatum { get; set; }

        //Navigatieproperty
        public virtual Persoon Persoon { get; set; }
    }
}
