using Boekingssysteem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boekingssysteem.ViewModels
{
    public class FunctieCRUDViewModel
    {
        public int FunctieID { get; set; }

        [Required(ErrorMessage = "Naam moet ingevuld zijn!")]
        public string Naam { get; set; }

        public ICollection<Functie> Functies { get; set; }
        public List<Functie> lijstFuncties { get; set; }

        //Navigatieproperty
        public virtual ICollection<PersoonFunctie> PersoonFuncties { get; set; }
    }
}
