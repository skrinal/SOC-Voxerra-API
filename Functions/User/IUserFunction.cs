namespace Voxerra_API.Functions.User
{
    public interface IUserFunction
    {
        User? Authenticate(string userName, string password);
        User GetUserById(int id);
    }
}
