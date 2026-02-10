using QMCH.Components.Pages;

namespace QMCH.Services
{
    public interface IUserService
    {
        Task<List<AdminUsers.UserListItem>> GetUsersAsync();
        Task<List<string>> CreateUserAsync(string firstName, string lastName, string email, string password, string role);
        Task DeleteUserAsync(string id);

        Task<List<AdminRoles.RoleItem>> GetRolesAsync();
        Task CreateRoleAsync(string roleName);
        Task DeleteRoleAsync(string id);
    }
}
