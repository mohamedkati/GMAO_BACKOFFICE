using GMAO.Application.Common.Exceptions;
using GMAO.Application.Helpers.Responses;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GMAO.Application.Common.Behaviours
{
    public class ExceptionHandlerMiddelware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddelware(RequestDelegate request)
        {
            this._next = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                   {
                       new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                   },
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                };
                var responseModel = ResponseResult<string>.FailResult(error.Message);

                switch (error)
                {
                    case UnAuthenticatedException e:
                        // unauthorized error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case UnAuthorizedException e:
                        // forbidden error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case AppValidationException e:
                        {
                            // custom application error
                            var validationResponse = ValidationResponse.Result(e.Errors.ToDictionary());
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            var validationResult = JsonSerializer.Serialize<ValidationResponse>(validationResponse, jsonOptions);
                            await response.WriteAsync(validationResult);
                            return;
                        }
                        break;
                    //case GridValidationException e:
                    //    {
                    //        var gridValidationResponse = new GridValidationErrorResponse { Succeeded = false, Message = error.Message };
                    //        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //        gridValidationResponse.Data = e.Errors;
                    //        var validationResult = JsonSerializer.Serialize(gridValidationResponse);
                    //        await response.WriteAsync(validationResult);
                    //        return;
                    //    }
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //_logger.Error(error, $"Message : {0}{Environment.NewLine}Inner Message : {1}{Environment.NewLine}", 
                        //    error.Message, error.InnerException?.Message);
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel, jsonOptions);

                await response.WriteAsync(result);
            }
        }
    }
}
