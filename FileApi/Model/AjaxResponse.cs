using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileApi.Model
{
    public class AjaxResponse
    {
        public int Code { get; set; }
        public dynamic Message { get; set; }
    }
}
