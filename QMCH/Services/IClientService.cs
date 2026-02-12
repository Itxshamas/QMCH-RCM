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
        Task<ClientType?> GetClientTypeByIdAsync(int id);
        Task CreateClientTypeAsync(ClientType item);
        Task UpdateClientTypeAsync(ClientType item);
        Task DeleteClientTypeAsync(int id);

        Task<List<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType?> GetServiceTypeByIdAsync(int id);
        Task CreateServiceTypeAsync(ServiceType item);
        Task UpdateServiceTypeAsync(ServiceType item);
        Task DeleteServiceTypeAsync(int id);
        Task CreateMultipleServiceTypesAsync(List<ServiceType> items);

        Task<List<Location>> GetLocationsAsync();
        Task CreateLocationAsync(Location item);
        Task DeleteLocationAsync(int id);
    }
}
