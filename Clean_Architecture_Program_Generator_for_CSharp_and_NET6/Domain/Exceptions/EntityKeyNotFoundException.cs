/////////////////////////////////////////////
// generated EntityKeyNotFoundException.cs //
/////////////////////////////////////////////
namespace Domain.Exceptions
{
    public sealed class EntityKeyNotFoundException : NotFoundException
    {
        public EntityKeyNotFoundException(string entity, string id)
            : base(String.Format("Entity {0} with the identifier {1} was not found.", entity, id))
        {
        }
    }
}
