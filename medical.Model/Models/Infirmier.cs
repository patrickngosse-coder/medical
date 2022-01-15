using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Infirmier:Personnel
    {
        

        [Display(Name = "Centre de Santé d'Appartenance")]
        public int? IDCEENTRE { get; set; }

        public virtual CentreSante CentreSante { get; set; }
        public virtual ICollection<ActeInfiemier> ActeInfiemiers { get; set; }
    }
}
