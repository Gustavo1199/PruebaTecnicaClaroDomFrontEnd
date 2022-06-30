using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroDom.Modelos
{
    public class VistaRegistro
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int pageCount { get; set; }
        public string excerpt { get; set; }
        public DateTime publishDate { get; set; }


        public string descriptiontemp { get; set; }
        public string excerpttemp { get; set; }
    }
}
