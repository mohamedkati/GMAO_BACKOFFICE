namespace GMAO.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("L’entité demandée est introuvable.")
        {
        }

        public NotFoundException(string name, object key)
            : base($"L’entité '{name}' ({key}) est introuvable.")
        {
        }
    }
}
