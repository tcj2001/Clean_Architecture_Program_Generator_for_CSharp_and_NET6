/////////////////////////////////////
// generated IRepositoryManager.cs //
/////////////////////////////////////
namespace Domain.Interfaces
{
    public interface IRepositoryManager
    {
        ISampleRepository SampleRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
