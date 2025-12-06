namespace GMAO.Application.Common.Exceptions
{
    public class UnAuthenticatedException : Exception
    {
        public UnAuthenticatedException()
        {

        }

        public UnAuthenticatedException(string message) : base(message)
        {

        }
    }
}
