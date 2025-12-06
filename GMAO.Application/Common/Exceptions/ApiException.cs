namespace GMAO.Application.Common.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception ex) : base(message, ex) { }
    }
}
