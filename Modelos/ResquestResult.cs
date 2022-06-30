using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClaroDom.Modelos
{
    public class ResquestResult
    {
        public Object result { get; set; }
        public bool isSuccess { get; set; }
        public Object error { get; set; }
        public HttpStatusCode status { get; set; }
    }
}
