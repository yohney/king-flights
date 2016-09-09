using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using King.FlightSearch.Services.External.Airports;
using RestSharp;

namespace King.FlightSearch.Services.External
{
    public class ExternalApiResult
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

        public bool IsSuccess
            => IsResponseStatusCodeOk((int)this.ResponseCode);

        public static ExternalApiResult<TResult> FromResponse<TResult>(IRestResponse<TResult> response)
            where TResult: class
        {
            if(!IsResponseStatusCodeOk((int)response.StatusCode))
            {
                return new ExternalApiResult<TResult>()
                {
                    ResponseCode = response.StatusCode,
                    ResponseMessage = response.Content
                };
            }

            return new ExternalApiResult<TResult>()
            {
                Data = response.Data,
                ResponseCode = response.StatusCode,
                ResponseMessage = response.Content
            };
        }

        private static bool IsResponseStatusCodeOk(int code) => (code / 100) == 2;
    }

    public class ExternalApiResult<TResult> : ExternalApiResult
    {
        public TResult Data { get; set; }
    }
}
