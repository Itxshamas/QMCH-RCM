using QMCH.Interfaces.Repositories;
using QMCH.Interfaces.Services;
using QMCH.Models;

namespace QMCH.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IGenericRepository<VisitDocumentation> _docRepository;

        public VisitService(IVisitRepository visitRepository, IGenericRepository<VisitDocumentation> docRepository)
        {
            _visitRepository = visitRepository;
            _docRepository = docRepository;
        }

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            return await _visitRepository.GetAllAsync();
        }

        public async Task<Visit?> GetVisitByIdAsync(int id)
        {
            return await _visitRepository.GetWithDetailsAsync(id);
        }

        public async Task CreateVisitAsync(Visit visit)
        {
            await _visitRepository.AddAsync(visit);
        }

        public async Task UpdateVisitAsync(Visit visit)
        {
            await _visitRepository.UpdateAsync(visit);
        }

        public async Task DeleteVisitAsync(int id)
        {
            await _visitRepository.DeleteAsync(id);
        }

        public async Task AddDocumentationAsync(VisitDocumentation documentation)
        {
            await _docRepository.AddAsync(documentation);
        }
    }
}
