using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QMCH.Components.Pages;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task<List<AdminUsers.UserListItem>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var items = new List<AdminUsers.UserListItem>();
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                items.Add(new AdminUsers.UserListItem
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email ?? "",
                    RoleName = roles.FirstOrDefault() ?? "User",
                    LastLogin = u.LastLoginDate,
                    IsActive = u.IsActive
                });
            }
            return items;
        }

        public async Task<List<string>> CreateUserAsync(string firstName, string lastName, string email, string password, string role)
        {
            var errors = new List<string>();
            var user = new User { FirstName = firstName, LastName = lastName, Email = email, UserName = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors.Select(e => e.Description));
                return errors;
            }

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
            return errors;
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null) await _userManager.DeleteAsync(user);
        }

        public async Task<List<AdminRoles.RoleItem>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var items = new List<AdminRoles.RoleItem>();
            foreach (var r in roles)
            {
                var count = (await _userManager.GetUsersInRoleAsync(r.Name!)).Count;
                items.Add(new AdminRoles.RoleItem { Id = r.Id, Name = r.Name!, UserCount = count });
            }
            return items;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null) await _roleManager.DeleteAsync(role);
        }
    }
}
