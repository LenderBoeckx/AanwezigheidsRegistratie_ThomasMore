using Boekingssysteem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boekingssysteem.ViewModels
{
    public class RichtingCRUDViewModel
    {
        public int RichtingID { get; set; }

        [Required(ErrorMessage = "Naam moet ingevuld zijn!")]
        public string Naam { get; set; }

        public ICollection<Richting> Richtingen { get; set; }
        public List<Richting> lijstRichtingen { get; set; }

        //Navigatieproperty
        public virtual ICollection<PersoonRichting> PersoonRichtingen { get; set; }
    }
}
