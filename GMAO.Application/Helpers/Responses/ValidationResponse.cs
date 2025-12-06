namespace GMAO.Application.Helpers.Responses
{
    public class ValidationResponse : ResponseResult<string>
    {
        public readonly Dictionary<string, string[]> _;

        public bool IsValidationError { get; protected set; }
        public Dictionary<string, string[]> Errors { get; protected set; }

        public static ValidationResponse Result(Dictionary<string, string[]> errors)
        {
            var response = new ValidationResponse();
            response.Data = "Des erreurs de validation ont été trouvées.";
            response.IsValidationError = true;
            response.IsSucceeded = false;
            response.Errors = errors;
            response.ErrorMessage = "Des erreurs de validation ont été trouvées.";

            return response;
        }
    }
}
