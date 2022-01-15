using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.ViewModel
{
    public class FacturationJournaliere
    {
        public DateTime JourS { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "n/a", ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double PaiementSomme { get; set; }
    }
}
