using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; // Ensure this is present
using QMCH.Models;
using System.Security.Claims;

namespace QMCH.Services
{
    // This is a placeholder for a more complete implementation
    // For a real application, you would implement custom authentication state persistence
    // For now, this will allow the application to compile and run with basic Identity setup
    public class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PersistingRevalidatingAuthenticationStateProvider> _logger;
        private readonly IOptions<IdentityOptions> _identityOptions; 
        public PersistingRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            IOptions<IdentityOptions> identityOptions) 
            : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = loggerFactory.CreateLogger<PersistingRevalidatingAuthenticationStateProvider>();
            _identityOptions = identityOptions; // Added
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            // Get the user manager from a new scope to ensure it's not disposed
            await using var scope = _scopeFactory.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            return await ValidateSecurityStampAsync(userManager, authenticationState.User);
        }

        private async Task<bool> ValidateSecurityStampAsync(UserManager<User> userManager, ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user == null)
            {
                return false;
            }
            else if (!userManager.SupportsUserSecurityStamp)
            {
                return true;
            }
            else
            {
                var principalStamp = principal.FindFirstValue(_identityOptions.Value.ClaimsIdentity.SecurityStampClaimType); // Modified
                var userStamp = await userManager.GetSecurityStampAsync(user);
                return principalStamp == userStamp;
            }
        }
    }
}
