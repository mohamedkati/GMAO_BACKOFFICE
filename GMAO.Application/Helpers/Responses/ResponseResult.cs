using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GMAO.Application.Helpers.Responses
{
    public class ResponseResult<T>
    {
        protected T Data { get; set; }
        protected bool IsSucceeded { get; set; }
        protected string ErrorMessage { get; set; }


        public static ResponseResult<T> OkResult(T data)
        {
            var response = new ResponseResult<T>();
            response.Data = data;
            response.IsSucceeded = true;
            return response;
        }

        public static ResponseResult<T> FailResult(string message)
        {
            var response = new ResponseResult<T>();
            response.ErrorMessage = message;
            response.IsSucceeded = true;
            return response;
        }

    }
}
