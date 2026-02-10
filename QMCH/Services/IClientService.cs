using QMCH.Models;

namespace QMCH.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task CreateAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task<int> GetCountAsync();

        Task<List<ClientType>> GetClientTypesAsync();
        Task CreateClientTypeAsync(ClientType item);
        Task DeleteClientTypeAsync(int id);

        Task<List<ServiceType>> GetServiceTypesAsync();
        Task CreateServiceTypeAsync(ServiceType item);
        Task DeleteServiceTypeAsync(int id);

        Task<List<Location>> GetLocationsAsync();
        Task CreateLocationAsync(Location item);
        Task DeleteLocationAsync(int id);
    }
}
