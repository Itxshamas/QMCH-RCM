using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _db;

        public ClientService(ApplicationDbContext db) { _db = db; }

        public async Task<List<Client>> GetAllAsync() => await _db.Clients.OrderByDescending(c => c.CreatedAt).ToListAsync();
        public async Task<Client?> GetByIdAsync(int id) => await _db.Clients.FindAsync(id);
        public async Task CreateAsync(Client client) { client.CreatedAt = DateTime.Now; _db.Clients.Add(client); await _db.SaveChangesAsync(); }
        public async Task UpdateAsync(Client client) { client.UpdatedAt = DateTime.Now; _db.Clients.Update(client); await _db.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var e = await _db.Clients.FindAsync(id); if (e != null) { _db.Clients.Remove(e); await _db.SaveChangesAsync(); } }
        public async Task<int> GetCountAsync() => await _db.Clients.CountAsync();

        public async Task<List<ClientType>> GetClientTypesAsync() => await _db.ClientTypes.OrderBy(c => c.ShortDescription).ToListAsync();
        public async Task<ClientType?> GetClientTypeByIdAsync(int id) => await _db.ClientTypes.FindAsync(id);
        public async Task CreateClientTypeAsync(ClientType item) { _db.ClientTypes.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateClientTypeAsync(ClientType item) { _db.ClientTypes.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteClientTypeAsync(int id) { var e = await _db.ClientTypes.FindAsync(id); if (e != null) { _db.ClientTypes.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<List<ServiceType>> GetServiceTypesAsync() => await _db.ServiceTypes.OrderBy(c => c.Description).ToListAsync();
        public async Task<ServiceType?> GetServiceTypeByIdAsync(int id) => await _db.ServiceTypes.FindAsync(id);
        public async Task CreateServiceTypeAsync(ServiceType item) { _db.ServiceTypes.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateServiceTypeAsync(ServiceType item) { _db.ServiceTypes.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteServiceTypeAsync(int id) { var e = await _db.ServiceTypes.FindAsync(id); if (e != null) { _db.ServiceTypes.Remove(e); await _db.SaveChangesAsync(); } }
        public async Task CreateMultipleServiceTypesAsync(List<ServiceType> items) { _db.ServiceTypes.AddRange(items); await _db.SaveChangesAsync(); }

        public async Task<List<Location>> GetLocationsAsync() => await _db.Locations.OrderBy(c => c.Name).ToListAsync();
        public async Task CreateLocationAsync(Location item) { _db.Locations.Add(item); await _db.SaveChangesAsync(); }
        public async Task DeleteLocationAsync(int id) { var e = await _db.Locations.FindAsync(id); if (e != null) { _db.Locations.Remove(e); await _db.SaveChangesAsync(); } }
    }
}