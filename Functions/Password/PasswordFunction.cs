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
        
        public async Task<bool> ResetPassword(string email)
        {
            var user = _chatAppContext.Tblusers.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                var resetToken = GeneratePasswordResetToken(email);

                var details = new EmailDetails
                {
                    Code = resetToken,
                    ToEmail = email,
                    Subject = "Reset Password Code",
                    PasswordEmail = true
                };

                await _emailMessage.SendEmail(details);
                
                return true;
            }

            return false;  // Invalid or expired token
        }

        public int GeneratePasswordResetToken(string email)
        {
            var token = _userRegistrationFunction.GenerateCode(); 

            var pendingPassword = new TblPendingPassword
            {
                Code = token,
                Email = email,
                ExpireTime = DateTime.UtcNow.AddMinutes(5)
            };

            _chatAppContext.Tblpendingpassword.Add(pendingPassword);
            _chatAppContext.SaveChanges();

            return token;
        }

        public async Task<bool> ChangePasswordUsingCode(string email, int code, string newPassword)
        {
            var resetToken = _chatAppContext.Tblpendingpassword.FirstOrDefault(x => (x.Code == code && x.Email == email));

            //
            if (resetToken == null || resetToken.ExpireTime < DateTime.UtcNow)
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
