using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public enum Categorie
    {
        Conventionné,
        Non_Conventionné
    }
    public enum Jour
    {
        Ouvrable,
        Non_Ouvrable
    }
    public  class Facturation
    {
        public Facturation()
        {

            Random rdm = new Random();
            DATEFACTURE = DateTime.Now;
            DateJour = DateTime.Today;
            DateSemaine = DateTime.Now.DayOfWeek;
            DateAnne = DateTime.Now.Year;
            REFERENCE = rdm.Next(1, 1000).ToString() + "" + DateTime.Now.Day.ToString() + "" + DateTime.Now.Month.ToString() + "" + DateTime.Now.Year.ToString();
        }

        [Key]
        [ScaffoldColumn(false)]
        public System.Guid IDFACTURATION { get; set; }

        public string REFERENCE { get; set; }

        public DateTime DATEFACTURE { get; set; }

        public DateTime DateJour { get; set; }

        public DayOfWeek DateSemaine { get; set; }

        public int DateAnne { get; set; }

        [Display(Name = "Patient")]
        public System.Guid IDPATIENT { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Catégorie")]
        public Categorie? CATEGORIE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Jour")]
        public Jour? JOUR { get; set; }

        [Display(Name = "Service")]
        public string SERVICE { get; set; }

        [Display(Name = "Examen")]
        public string EXAMEN { get; set; }

        [Display(Name = "Prix")]
        public decimal PRIX { get; set; }

        [Display(Name = "Médecin Examinateur")]
        public string EXAMINATEUR { get; set; }

        [Display(Name = "Médecin Demandeur")]
        public string DEMANDEUR { get; set; }
 
     
        public virtual Patient Patient { get; set; }
        public virtual Medecin Medecin { get; set; }
        public virtual Demandeur Demandeur { get; set; }
        public virtual Examen Examen { get; set; }

    }
}
