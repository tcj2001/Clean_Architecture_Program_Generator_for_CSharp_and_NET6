/////////////////////////////////////
// generated SampleService.cs //
/////////////////////////////////////
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class SampleService : ISampleService
    {
        private readonly IRepositoryManager _repositoryManager;

        public SampleService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<(IEnumerable<Sample> entities, string Message)> FindSample(Expression<Func<Sample, bool>> expression, CancellationToken cancellationToken = default)
        {
            var items = await _repositoryManager.SampleRepository.Find(expression);
            return (items, "Sample records retrieved");
        }

        public async Task<(IEnumerable<Sample> entities, string Message)> GetAllSample(CancellationToken cancellationToken = default)
        {
            var items = await _repositoryManager.SampleRepository.GetAll();
            return (items, "Sample records retrieved");
        }

        public async Task<(Sample? entity, string Message)> GetSampleById(System.Int32 Id, CancellationToken cancellationToken = default)
        {
            var item = await _repositoryManager.SampleRepository.GetById(Id);
            if (item == null)
            {
                throw new EntityKeyNotFoundException("Sample", Id.ToString());
            }
            return (item, "Sample record retrieved");
        }

        public async Task<string> AddSample(Sample entity, CancellationToken cancellationToken = default)
        {
            if (entity != null)
            {
                var item = await _repositoryManager.SampleRepository.GetById(entity.Id);
                if (item != null)
                {
                    throw new EntityKeyFoundException("Sample", entity.Id.ToString());
                }
                else
                {
                    await _repositoryManager.SampleRepository.Add(entity);
                    await _repositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    return "Sample record added";
                }
            }
            throw new Exception("Add Error");
        }

        public async Task<string> RemoveSample(System.Int32 Id, CancellationToken cancellationToken = default)
        {
            var item = await _repositoryManager.SampleRepository.GetById(Id);
            if (item == null)
            {
                throw new EntityKeyNotFoundException("Sample", Id.ToString());
            }
            else
            {
                _repositoryManager.SampleRepository.Remove(item);
                await _repositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return "Sample record deleted";
            }
        }

        public async Task<string> UpdateSample(Sample entity, CancellationToken cancellationToken = default)
        {
            if (entity != null)
            {
                var item = await _repositoryManager.SampleRepository.GetById(entity.Id);
                if (item == null)
                {
                    throw new EntityKeyNotFoundException("Sample", entity.Id.ToString());
                }
                else
                {
                    //only place that need change if structure changes
                    //item.Name = entity.Name;
                    //item.Description = entity.Description;

                    _repositoryManager.SampleRepository.Update(item);
                    await _repositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    return "Sample record updated";
                }
            }
            throw new Exception("Update Error");
        }
    }
}
