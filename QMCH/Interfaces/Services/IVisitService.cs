using QMCH.Models;

namespace QMCH.Interfaces.Services
{
    public interface IVisitService
    {
        Task<IEnumerable<Visit>> GetAllVisitsAsync();
        Task<Visit?> GetVisitByIdAsync(int id);
        Task CreateVisitAsync(Visit visit);
        Task UpdateVisitAsync(Visit visit);
        Task DeleteVisitAsync(int id);
        Task AddDocumentationAsync(VisitDocumentation documentation);
    }
}
