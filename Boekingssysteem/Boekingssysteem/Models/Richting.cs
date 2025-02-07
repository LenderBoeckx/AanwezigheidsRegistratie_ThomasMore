using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boekingssysteem.Models
{
    public class Richting
    {
        public int RichtingID { get; set; }

        [Required(ErrorMessage = "Naam moet ingevuld zijn!")]
        public string Naam { get; set; }

        //Navigatieproperty
        public virtual ICollection<PersoonRichting> PersoonRichtingen { get; set; }
    }
}
