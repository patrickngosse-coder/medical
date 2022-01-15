using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Province
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDPROVINCE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        public virtual ICollection<Territoire> Territoires { get; set; }
        public virtual ICollection<DivisionProvincialSante> DivisionProvincialSantes { get; set; }
    }
}
