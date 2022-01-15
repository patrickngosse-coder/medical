using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public enum Compte
    {

    }
    public class Depense
    {
        public Depense()
        {
            DateEdition = DateTime.Now;
            DateDay = DateTime.Today;
            DateWeek = DateTime.Now.DayOfWeek;
            DateYear = DateTime.Now.Year;
        }

        [Key]
        [ScaffoldColumn(false)]
        public int DepenseID { get; set; }

        public DateTime DateEdition { get; set; }

        [Display(Name = "Bénéficiaire")]
        public string Destinateur { get; set; }

        [Display(Name = "Motif")]
        [DataType(DataType.MultilineText)]
        public string Motif { get; set; }

        [Display(Name = "Montant")]
        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "n/a", ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double Montant { get; set; }

        public DateTime DateDay { get; set; }

        public DayOfWeek DateWeek { get; set; }

        public int DateYear { get; set; }
    }
}

