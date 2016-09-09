using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King.FlightSearch.Services.Infrastructure
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }

        public ServiceResult()
        {
            this.Errors = new List<string>();
        }
    }

    public class ServiceResult<TData> : ServiceResult
        where TData: class, new()
    {
        public TData Data { get; set; }
    }
}
