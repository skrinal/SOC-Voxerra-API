using Voxerra_API.Entities;

namespace Voxerra_API.Functions.Registration
{
    public class UserRegistrationFunction : IUserRegistrationFunction
    {
        private readonly ChatAppContext _chatAppContext;

        public UserRegistrationFunction(ChatAppContext chatAppContext)
        {
            _chatAppContext = chatAppContext;
        }

        public UserRegistration? Registration(string loginId, string password)
        {
            try
            {
                //var entity = _chatAppContext.TblUsers.Single(x => x.LoginId == loginId);
                //if (entity == null) return null;

                //var isPasswordMatched = VerifityPassword(password, entity.StoredSalt, entity.Password);
                //if (!isPasswordMatched) return null;

                //var token = GenerateJwtToken(entity);

                //return new User
                //{
                //    Id = entity.Id,
                //    UserName = entity.UserName,
                //    Token = token
                //};
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
