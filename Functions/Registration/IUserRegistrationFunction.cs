namespace Voxerra_API.Functions.Registration
{
    public interface IUserRegistrationFunction
    {
        Task<int> Registration(string userName, string password, string email);
    }
}
