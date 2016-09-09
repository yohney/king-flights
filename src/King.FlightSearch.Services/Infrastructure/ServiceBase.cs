using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King.FlightSearch.Services.Infrastructure
{
    public abstract class ServiceBase
    {
        public ServiceResult Ok()
        {
            return new ServiceResult()
            {
                IsSuccess = true
            };
        }

        public ServiceResult<TData> Ok<TData>(TData data)
            where TData: class, new()
        {
            return new ServiceResult<TData>()
            {
                Data = data,
                IsSuccess = true
            };
        }

        public ServiceResult Error(params string[] errors)
        {
            return new ServiceResult()
            {
                IsSuccess = false,
                Errors = errors.ToList()
            };
        }

        public ServiceResult<TData> Error<TData>(params string[] errors)
            where TData : class, new()
        {
            return new ServiceResult<TData>()
            {
                IsSuccess = false,
                Errors = errors.ToList()
            };
        }
    }
}
