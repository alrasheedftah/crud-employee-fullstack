using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Resource.Responses
{
    public class SuccessResponse
    {
        public int code { get; set; }
        public object Result { get; set; }
        public string MessageResult { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; } = true;
    }

}
