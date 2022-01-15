using medical.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.ViewModel
{
    public class IndexPatient
    {
        public IEnumerable<Facturation> Facturations { get; set; }
        public IEnumerable<Examen> Examens { get; set; }
        public IEnumerable<Serve> Services { get; set; }
    }
}
