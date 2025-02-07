using Boekingssysteem.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Boekingssysteem.ViewModels
{
    public class AfwezigheidCRUDViewModel
    {
        public int AfwezigheidID { get; set; }

        [Required(ErrorMessage = "Personeelnummer moet ingevuld zijn!")]
        [MinLength(8, ErrorMessage = "Personeelnummer moet 8 tekens zijn!")]
        [MaxLength(8, ErrorMessage = "Personeelnummer moet 8 tekens zijn!")]
        public string Personeelnummer { get; set; }

        [Required(ErrorMessage = "Begindatum moet ingevuld zijn!")]
        [DataType(DataType.Date)]
        public DateTime Begindatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Einddatum { get; set; }

        //Navigatieproperty
        public virtual Persoon Persoon { get; set; }

        public virtual ICollection<Persoon> Personen { get; set; }
    }
}
