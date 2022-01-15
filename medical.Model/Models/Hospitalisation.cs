using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public class Hospitalisation
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDHOSPI { get; set; }

        [Required]
        [Display(Name = "Pavillon d'Appartenance")]
        public int? IDPAVILLON { get; set; }

        [Required]
        [Display(Name = "Patient")]
        public int? IDPATIENT { get; set; }

        [Required]
        [DataType(DataType.Custom)]
        [Display(Name = "Nombre Jour")]
        public double NBRJOUR { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Prix Hosp/Jour")]
        public double PRIXUNITAIRE { get; set; }

        public double CoutTotal
        {
            get
            {
                return NBRJOUR * PRIXUNITAIRE;
            }
        }


        public virtual Pavillon Pavillon { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
