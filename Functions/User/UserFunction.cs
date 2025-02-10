using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voxerra_API.Controllers.Authenticate;

namespace Voxerra_API.Functions.User
{
    public class UserFunction(ChatAppContext chatAppContext, IEmailFunction emailFunction) : IUserFunction
    {
        private readonly ChatAppContext _chatAppContext = chatAppContext;
        private readonly IEmailFunction _emailFunction = emailFunction;
        public async Task<AuthenticateResponse?> Authenticate(string userName, string password)
        {
            try
            {
                var entity = await _chatAppContext.Tblusers
                    .FirstOrDefaultAsync(x => x.UserName == userName);

                if (entity == null) return null;

                var isPasswordMatched = VerifyPassword(password, entity.StoredSalt, entity.Password);
                if (!isPasswordMatched) return null;

                var twoAuthEnable = await _chatAppContext.Tblusersettings
                    .Where(x => x.UserId == entity.Id)
                    .Select(x => x.TwoFactorEnabled)
                    .FirstOrDefaultAsync();
                
                if(twoAuthEnable)
                {
                    var AuthCode = _emailFunction.GenerateCode();
                    var emailPrompt = new EmailDetails
                    {
                        ToEmail = entity.Email,
                        Subject = "Two-Factor Auth Code",
                        Code = AuthCode,
                        TwoAuthEmail = true
                        
                    };
                    _emailFunction.SendEmail(emailPrompt);

                    var AuthRequest = new TblTwoFactorAuth
                    {
                        Id = entity.Id,
                        Code = AuthCode,
                        ExpireTime =  DateTime.UtcNow.AddMinutes(5)
                    };
                    
                    _chatAppContext.Tbltwofactorauth.Add(AuthRequest);
                    await _chatAppContext.SaveChangesAsync();
                    
                    return new AuthenticateResponse
                    {
                        Id = entity.Id,
                        RequiresTwoFactorAuth = true
                    };
                }
                
                var token = GenerateJwtToken(entity);

                return new AuthenticateResponse
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Token = token,
                    RequiresTwoFactorAuth = false
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public async Task<AuthenticateResponse?> TwoFactorAuth(int userId, int code)
        {
            try
            {
                var existingAuth = await _chatAppContext.Tbltwofactorauth
                    .Where(x => x.Id == userId 
                                && x.ExpireTime > DateTime.UtcNow
                                && x.Code == code) 
                    .OrderByDescending(x => x.ExpireTime)
                    .FirstOrDefaultAsync();

                if (existingAuth == null) return null;
                
                var entity = await _chatAppContext.Tblusers
                    .FirstOrDefaultAsync(x => x.Id == userId);
                
                if (entity == null) return null;
                
                var token = GenerateJwtToken(entity);

                return new AuthenticateResponse
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Token = token,
                    RequiresTwoFactorAuth = false
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public User GetUserById(int id)
        {
            var entity = _chatAppContext.Tblusers.FirstOrDefault(x => x.Id == id);

            if (entity == null) return new User();

            return new User
            {
                UserName = entity.UserName,
                Id = entity.Id,
                AvatarSourceName = entity.AvatarSourceName,
                IsOnline = entity.IsOnline,
                LastLogonTime = entity.LastLogonTime,
                CreationYear = entity.CreationDate.Year.ToString()
            };
        }
        private static bool VerifyPassword(string enteredPassword, byte[] storedSalt, string storedPassword)
        {
            const int iterationCount = 10000; 
            const int keySize = 256 / 8;
            
            var encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA1, //HMACSHA1 / HMACSHA256
                iterationCount: iterationCount,
                numBytesRequested: keySize));
            
            return encryptedPassword.Equals(storedPassword);
        }
        private static string GenerateJwtToken(TblUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("rweofkwurtihonmoiurwhbnrtwrgwrgjge");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("id", user.Id.ToString())]),
                Expires = DateTime.UtcNow.AddDays(1),             
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
       
    }
}
