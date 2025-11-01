using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Helpers.Responses
{
    public class ValidationResponse : ResponseResult<Dictionary<string, string[]>>
    {
        public readonly Dictionary<string, string[]> _;

        public bool IsValidationError { get; protected set; }

        public static ValidationResponse Result(Dictionary<string, string[]> errors)
        {
            var response = new ValidationResponse();
            response.Data = errors;
            response.IsValidationError = true;
            response.IsSucceeded = false;
            response.ErrorMessage = "Des erreurs de validation ont été trouvées.";

            return response;
        }
    }
}
