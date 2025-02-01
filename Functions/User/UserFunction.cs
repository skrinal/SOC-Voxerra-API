using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voxerra_API.Entities;

namespace Voxerra_API.Functions.User
{
    public class UserFunction(ChatAppContext chatAppContext) : IUserFunction
    {
        private readonly ChatAppContext _chatAppContext = chatAppContext;
        public async Task<User?> Authenticate(string userName, string password)
        {
            try
            {
                var entity = await _chatAppContext.Tblusers
                    .FirstOrDefaultAsync(x => x.UserName == userName);

                if (entity == null) return null;

                var isPasswordMatched = VerifyPassword(password, entity.StoredSalt, entity.Password);
                if (!isPasswordMatched) return null;

                var token = GenerateJwtToken(entity);

                return new User
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Token = token
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
