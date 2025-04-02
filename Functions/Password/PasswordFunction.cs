using Voxerra_API.Controllers.Password;
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
                    PasswordEmail = true
                };

                await _emailMessage.SendEmail(details);
                
                return true;
            }

            return false;  
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

        public async Task<Guid> ValidateCode(int code, string email)
        {
            var validCodeCheck = await _chatAppContext.Tblpendingpassword
                .FirstOrDefaultAsync(x => x.Email == email
                                        && x.Code == code 
                                        && x.ExpireTime > DateTime.UtcNow);
            
            if (validCodeCheck == null) return Guid.Empty;
            
            validCodeCheck.AuthString = Guid.NewGuid();
            
            _chatAppContext.Tblpendingpassword.Update(validCodeCheck);
            await _chatAppContext.SaveChangesAsync();
            
            return validCodeCheck.AuthString;
        }
        public async Task<PassCResult> ChangePasswordUsingCode(Guid guidAuth, string newPassword)
        {
            var resetToken = await _chatAppContext.Tblpendingpassword
                .SingleOrDefaultAsync(x => x.AuthString == guidAuth);

            if (resetToken == null ) return new PassCResult { Success = false };

            var user = await _chatAppContext.Tblusers
                .SingleOrDefaultAsync(x => x.Email == resetToken.Email);
            if (user == null) return new PassCResult { Success = false };
            
            var passwordHash = _userRegistrationFunction.EncryptPassword(newPassword);
            user.Password = passwordHash.encryptedPassword;
            user.StoredSalt = passwordHash.salt;
            
            _chatAppContext.Tblpendingpassword.Remove(resetToken); // Remove used token
            _chatAppContext.Tblusers.Update(user);
            await _chatAppContext.SaveChangesAsync();

            return new PassCResult { Success = true, Email = resetToken.Email };
        }
        public async Task<bool> SendCodeAgain(string email)
        {
            var resetToken = _chatAppContext.Tblpendingpassword.FirstOrDefault(x => (x.Email == email));

            if (resetToken != null )
            {
                resetToken.Code = _userRegistrationFunction.GenerateCode();
                resetToken.ExpireTime = DateTime.UtcNow.AddMinutes(5);
                
                var details = new EmailDetails
                {
                    Code = resetToken.Code,
                    ToEmail = email,
                    PasswordEmail = true
                };

                await _emailMessage.SendEmail(details);
                
                _chatAppContext.Tblpendingpassword.Update(resetToken);
                await _chatAppContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
