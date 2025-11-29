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
        public T Data { get; protected set; }
        public bool IsSucceeded { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public string Message { get; set; }

        public static ResponseResult<T> OkResult(T data, string message = "")
        {
            var response = new ResponseResult<T>();
            response.Data = data;
            response.IsSucceeded = true;
            response.Message = message;
            return response;
        }

        public static ResponseResult<T> FailResult(string message)
        {
            var response = new ResponseResult<T>();
            response.ErrorMessage = message;
            response.IsSucceeded = false;
            return response;
        }

    }
}
