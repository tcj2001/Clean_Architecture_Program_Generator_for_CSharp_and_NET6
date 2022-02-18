//////////////////////////////////////////
// generated EntityKeyFoundException.cs //
//////////////////////////////////////////
namespace Domain.Exceptions
{
    public sealed class EntityKeyFoundException : BadRequestException
    {
        public EntityKeyFoundException(string entity, string id)
            : base(String.Format("Entity {0} with the identifier {1} exist.", entity, id))
        {
        }
    }
}
