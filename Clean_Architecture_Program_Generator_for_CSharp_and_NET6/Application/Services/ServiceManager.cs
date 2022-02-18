/////////////////////////////////
// generated ServiceManager.cs //
/////////////////////////////////
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ISampleService> _lazySampleService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazySampleService = new Lazy<ISampleService>(() => new SampleService(repositoryManager));
        }
        public ISampleService SampleService => _lazySampleService.Value;
    }
}
