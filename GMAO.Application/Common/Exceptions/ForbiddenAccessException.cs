namespace GMAO.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException()
            : base("Vous n’avez pas la permission d’accéder à cette ressource.")
        {
        }

        public ForbiddenAccessException(string message)
            : base(message)
        {
        }
    }
}
