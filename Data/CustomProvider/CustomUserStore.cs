using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using Blazor4.Data.CustomProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Blazor4.CustomProvider
{
    public class CustomUserStore : IUserStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        private readonly ApplicationUsersTable _usersTable;
        //private readonly UserManager<ApplicationUser> userManager;

        public CustomUserStore(ApplicationUsersTable usersTable)
        {
            _usersTable = usersTable;
            //this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await _usersTable.CreateAsync(user);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Dispose resources if necessary
        }

        public async Task<ApplicationUser> FindByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return await _usersTable.FindByIdAsync(Email);
        }

        public async Task<ApplicationUser> FindByIdAsync(string Email, CancellationToken cancellationToken)
        {
            return await _usersTable.FindByIdAsync(Email);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _usersTable.FindByNameAsync(userName);
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public async Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return true;
        }

        

        public async Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.Email.ToUpper();
            //return await userManager.NormalizeEmail(user);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            return hasher.HashPassword(user, user.Email);
            //return (await _usersTable.FindByIdAsync(user.Email)).PasswordHash;
            //return Task.FromResult(user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
           
            return await Task.FromResult(user.Email); 
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
           //throw new NotImplementedException();
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

       

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            // Set the email address of the user
            user.Email = email;

            // Save the changes to the user
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            // Set the email confirmation status of the user
            user.EmailConfirmed = confirmed;
            // Save the changes to the user
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            // Set the normalized email for the user
            user.Email = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
           // throw new NotImplementedException();
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
