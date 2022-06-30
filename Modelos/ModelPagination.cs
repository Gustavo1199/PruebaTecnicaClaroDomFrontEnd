using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroDom.Modelos
{
    public class ModelPagination
    {
        public List<VistaRegistro> Valores { get; set; }

        public int CurrentPageIndex { get; set; }


        public int PageCount { get; set; }
    }
}
