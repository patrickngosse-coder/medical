using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public class Serve
    {
       
        [Key]
        [ScaffoldColumn(false)]
        public int IDSERVICE { get; set; }

        //[Required]
        //[Display(Name = "Hopital d'Appartenance")]
        //public int? IDHOPITAL { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        [Display(Name = "Pourcentage Examinateur")]
        public double POUCENTAGEXAMEN { get; set; }
         
       
        [Display(Name = "Pourcentage Demandeur")]
        public double POUCENTAGEDEMAND { get; set; }

        public double PourcentageDem
        {
            get
            {
                return POUCENTAGEDEMAND;
            }
        }

        public double PourcentageExam
        {
            get
            {
                return POUCENTAGEXAMEN;
            }
        }

        public virtual ICollection<Examen> Examens { get; set; }
        public virtual ICollection<Materiel> Materiels { get; set; }
        public virtual ICollection<Pavillon> Pavillons { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        //public virtual Hopital Hopital { get; set; }
    }
}
