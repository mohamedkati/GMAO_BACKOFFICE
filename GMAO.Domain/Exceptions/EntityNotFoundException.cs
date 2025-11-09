namespace GMAO.Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entityName, object entityId)
            : base($"{entityName} with ID {entityId} was not found")
        {
        }
    }

}
