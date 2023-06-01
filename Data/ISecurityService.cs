using Blazor4.Models;


namespace Blazor4.Data
{
    public interface ISecurityService
    {
        bool ValidateCredentials(string password, Password dbPassword);
    }
}