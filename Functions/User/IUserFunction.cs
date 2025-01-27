namespace Voxerra_API.Functions.User
{
    public interface IUserFunction
    {
        Task<User?> Authenticate(string loginId, string password);
        User GetUserById(int id);
    }
}
