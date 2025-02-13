namespace Voxerra_API.Functions.User
{
    public interface IUserFunction
    {
        Task<AuthenticateResponse?> Authenticate(string userName, string password, string ipAdress);
        User GetUserById(int id);
        Task<AuthenticateResponse?> TwoFactorAuth(int userId, int code, string ipAdress); 
    }
}
