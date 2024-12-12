using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Functions.Registration
{
    public class UserRegistrationFunction : IUserRegistrationFunction
    {
        private readonly ChatAppContext _chatAppContext;
        //private readonly EmailMessage _emailMessage;
        private int verificationCode;
        public UserRegistrationFunction(ChatAppContext chatAppContext/*, EmailMessage emailMessage*/)
        {
            _chatAppContext = chatAppContext;
            //_emailMessage = emailMessage;
        }

      
        public async Task<bool> IsEmailUnique(string email)
        {
            return !await _chatAppContext.TblUsers.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserNameUnique(string userName)
        {
            return !await _chatAppContext.TblUsers.AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> Registration(string userName, string password, string email)
        {
            try
            {
                var (encryptedPassword, salt) = EncryptPassword(password);
                
                var newUser = new TblUser
                {
                    UserName = userName,
                    Email = email,
                    Password = encryptedPassword,
                    StoredSalt = salt,
                    AvatarSourceName = "default.png" // Pridat defualt image 
                };


                //var verificationCode = GenerateCode();
                // _emailMessage.SendEmail(email, verificationCode, userName);
                
                // treba spravit input od Clienta

                _chatAppContext.TblUsers.Add(newUser);
                var result = await _chatAppContext.SaveChangesAsync();

                return result == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public int GenerateCode()
        {
            Random random = new Random();
            return verificationCode = random.Next(1000000, 9999999);
        }

        public (string encryptedPassword, byte[] salt) EncryptPassword(string password)
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
