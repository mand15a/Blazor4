using Blazor4.Data.CustomProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Blazor4.Data.CustomProvider
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
                  errors, services, logger)
        {
        }

       /* public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return Task.FromResult(result == PasswordVerificationResult.Success);
        }*/
    }
}





























/*

     public bool ValidateCredentials(string password, Password dbPassword)
    {
        // Custom validation logic
        // For example, you can use a different hashing algorithm or add additional checks

        // Convert the stored salt and password hash from Base64 strings to byte arrays
        byte[] saltBytes = Convert.FromBase64String(dbPassword.PasswordSalt);
        byte[] storedPasswordHashBytes = Convert.FromBase64String(dbPassword.PasswordHash);

        // Generate the hash of the provided password using the stored salt
        byte[] providedPasswordBytes = Encoding.Unicode.GetBytes(password);
        byte[] providedPasswordHashBytes = Hash(providedPasswordBytes, saltBytes);

        // Compare the generated hash with the stored password hash
        bool valid = storedPasswordHashBytes.SequenceEqual(providedPasswordHashBytes);

        return valid;
    }
*/









//, ISecurityService<ValidateCredentials<TUser>>































/*using Blazor4.CustomProvider;
using Blazor4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Blazor4.Data;
using Microsoft.AspNet.Identity;

public class CustomUserManager<TUser> : UserManager<TUser> where TUser : IdentityUser

{
    private readonly AdventureWorks2019Context _dbContext;

    public CustomUserManager(IUserStore<TUser> store,AdventureWorks2019Context dbContext, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators,
        IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<Microsoft.AspNet.Identity.UserManager<TUser>> logger)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
              errors, services, logger)
    {
        //_signInManager = signInManager;
        _dbContext = dbContext;
    }

    public async Task<bool> CheckPasswordAsync(TUser user, string password)
    {
        var result = UserManager.CheckPasswordAsync(user, password, isPersistent: true, lockoutOnFailure: false);

        return true; 
    }
}*/



//var IsUserExist = 

// Retrieve the user's stored password hash and salt
/* string storedPasswordHash = user.PasswordHash;
 //string storedPasswordSalt = user.PasswordSalt;

 // Create an instance of the SecurityService class
 SecurityService securityService = new SecurityService();

 // Create a Password object with the stored password hash and salt
 Password dbPassword = new Password
 {
     PasswordHash = storedPasswordHash,
     //PasswordSalt = storedPasswordSalt
 };

 // Validate the credentials using the SecurityService's ValidateCredentials method
 bool valid = securityService.ValidateCredentials(password, dbPassword);

 return valid;*/










/* var passwordHasher = new PasswordHasher<TUser>();

         var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

         return Task.FromResult(result == PasswordVerificationResult.Success);*/