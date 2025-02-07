using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace Boekingssysteem.Models
{
    public class Functie
    {
        public int FunctieID { get; set; }
        
        [Required(ErrorMessage = "Naam moet ingevuld zijn!")]
        public string Naam { get; set; }

        //Navigatieproperty
        public virtual ICollection<PersoonFunctie> PersoonFuncties { get; set; }
    }
}
