namespace Voxerra_API.Functions.Registration
{
    public interface IUserRegistrationFunction
    {
        Task<bool> IsUserNameUnique(string userName);
        Task<bool> IsEmailUnique(string email);
        Task<bool> Registration(string loginId, string userName, string password, string email);
    }
}
