using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Functions.Registration
{
    public class UserRegistrationFunction : IUserRegistrationFunction
    {
        private readonly ChatAppContext _chatAppContext;

        public UserRegistrationFunction(ChatAppContext chatAppContext)
        {
            _chatAppContext = chatAppContext;
        }

        public async Task<int> Registration(string userName, string password, string email)
        {
            try
            {
                var entity = _chatAppContext.TblUsers.SingleOrDefault(x => x.Email == email);
                if (entity != null) return 0;

                var (encryptedPassword, salt) = EncryptPassword(password);
                
                var newUser = new TblUser
                {
                    Id = 1, //hj8yuh8uyh8yh78
                    UserName = userName,
                    Email = email,
                    Password = encryptedPassword,
                    StoredSalt = salt,
                    AvatarSourceName = "default.png"
                };

                _chatAppContext.TblUsers.Add(newUser);
                var result = await _chatAppContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }
        public static (string encryptedPassword, byte[] salt) EncryptPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            RandomNumberGenerator.Fill(salt);

            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (encryptedPassword, salt);
        }
    }
}
