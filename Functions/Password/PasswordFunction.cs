using Org.BouncyCastle.Asn1.Ocsp;
using System;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Registration;

namespace Voxerra_API.Functions.Password
{
    public class PasswordFunction(IUserRegistrationFunction userRegistrationFunction, IEmailFunction emailMessage, ChatAppContext chatAppContext) : IPasswordFunction
    {
        private readonly IUserRegistrationFunction _userRegistrationFunction = userRegistrationFunction;
        private readonly IEmailFunction _emailMessage = emailMessage;
        private readonly ChatAppContext _chatAppContext = chatAppContext;

        

        public async Task<bool> ChangePasswordUsingToken(string token, string newPassword)
        {
            var pendingChange = _chatAppContext.Tblpendingpassword.FirstOrDefault(x => x.Token == token);

            if (pendingChange != null)
            {
                //var user = _chatAppContext.Tblusers.FirstOrDefault(x => )

                
                return true;
            }

            return false;  // Invalid or expired token
        }

        public string GeneratePasswordResetToken(string email)
        {
            var token = Guid.NewGuid().ToString();
            var pendingPassword = new TblPendingPassword
            {
                Token = token,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            //_chatAppContext.Tblpendingpassword.Add(pendingPassword);
            //_chatAppContext.SaveChanges();

            return token;
        }

        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            var resetToken = _chatAppContext.Tblpendingpassword.FirstOrDefault(x => x.Token == token);

            //
            if (resetToken == null || resetToken.CreatedAt < DateTime.UtcNow)
            {
                return false; 
            }
            //

            var user = _chatAppContext.Tblusers.FirstOrDefault(x => x.Email == resetToken.Email);
            if (user == null)
            {
                return false; 
            }

          
            var passwordHash = _userRegistrationFunction.EncryptPassword(newPassword);

            user.Password = passwordHash.encryptedPassword;
            user.StoredSalt = passwordHash.salt;


            _chatAppContext.Tblpendingpassword.Remove(resetToken); // Remove used token
            _chatAppContext.Tblusers.Update(user);
            await _chatAppContext.SaveChangesAsync();

            return true; 
        }
    }
}
