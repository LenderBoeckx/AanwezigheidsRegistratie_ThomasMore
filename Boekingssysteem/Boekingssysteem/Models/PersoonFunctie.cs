using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boekingssysteem.Models
{
    public class PersoonFunctie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersoonFunctieID { get; set; }

        [MinLength(8)]
        [MaxLength(8)]
        [Required(ErrorMessage = "Personeelnummer moet ingevuld zijn!")]
        public string Personeelnummer { get; set; }

        [Required(ErrorMessage = "FunctieID moet ingevuld zijn!")]
        public int FunctieID { get; set; }

        //Navigatieproperties
        public virtual Persoon Persoon { get; set; }
        public virtual Functie Functie { get; set; }
    }
}
