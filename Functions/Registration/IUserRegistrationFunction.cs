namespace Voxerra_API.Functions.Registration
{
    public interface IUserRegistrationFunction
    {
        Task<bool> Registration(string loginId, string userName, string password, string email);
        Task<bool> IsEmailUnique(string email);
    }
}
