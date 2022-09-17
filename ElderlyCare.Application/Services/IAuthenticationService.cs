namespace ElderlyCare.Application.Services
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstName, string lastName, string email, string password);

    }
}
