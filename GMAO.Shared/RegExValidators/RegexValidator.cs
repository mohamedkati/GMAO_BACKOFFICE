namespace GMAO.Shared.RegExValidators
{
    public static class RegexValidator
    {
        public const string PASSWORD_VALIDATOR_REGEX = "^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
    }
}
