using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Functions.Registration
{
    public class UserRegistrationFunction(ChatAppContext chatAppContext, IEmailFunction emailMessage) : IUserRegistrationFunction
    {
        private readonly ChatAppContext _chatAppContext = chatAppContext;
        private readonly IEmailFunction _emailMessage = emailMessage;
        private int verificationCode;

        public async Task<bool> IsEmailUnique(string email)
        {
            return !await _chatAppContext.Tblusers.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserNameUnique(string userName)
        {
            return !await _chatAppContext.Tblusers.AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> Registration(string userName, string password, string email)
        {
            try
            {
                int verificationCode;
                var existingPendingUser = await _chatAppContext.Tblpendingusers.FirstOrDefaultAsync(x => x.Email == email);

                
                if (existingPendingUser != null)
                {
                    existingPendingUser.UserName = userName;
                    existingPendingUser.Password = EncryptPassword(password).encryptedPassword;
                    existingPendingUser.StoredSalt = EncryptPassword(password).salt;

                    verificationCode = GenerateCode();
                    
                    existingPendingUser.VerificationCode = verificationCode;
                    existingPendingUser.CreatedAt = DateTime.UtcNow; 
                }
                else
                {
                    var (encryptedPassword, salt) = EncryptPassword(password);
                    verificationCode = GenerateCode();

                    var pendingUser = new TblPendingUser
                    {
                        UserName = userName,
                        Email = email,
                        Password = encryptedPassword,
                        StoredSalt = salt,
                        VerificationCode = verificationCode,
                        CreatedAt = DateTime.UtcNow
                    };

                    _chatAppContext.Tblpendingusers.Add(pendingUser);
                }

                await _chatAppContext.SaveChangesAsync();

                string codeAsString = verificationCode.ToString();

                _emailMessage.SendEmail(email, "Verification Code", codeAsString);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public int GenerateCode()
        {
            Random random = new();
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
