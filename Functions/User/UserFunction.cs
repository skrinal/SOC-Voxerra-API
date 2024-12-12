using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Voxerra_API.Entities;

namespace Voxerra_API.Functions.User
{
    public class UserFunction : IUserFunction
    {
        private readonly ChatAppContext _chatAppContext;

        public UserFunction(ChatAppContext chatAppContext)
        {
            _chatAppContext = chatAppContext;
        }
        public User? Authenticate(string userName, string password)
        {
            try
            {
                var entity = _chatAppContext.TblUsers.Single(x => x.UserName == userName);
                if (entity == null) return null;

                var isPasswordMatched = VerifityPassword(password, entity.StoredSalt, entity.Password);
                if (!isPasswordMatched) return null;


                var accesToken = GenerateJwtToken(entity);
                var refreshToken = GenerateRefreshToken();

                var tokenStore = new TblRefreshTokens
                {
                    UserId = entity.Id,
                    RefreshToken = refreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(12)
                };
                //_chatAppContext.TblRefreshTokens.Add(tokenStore);
                //_chatAppContext.SaveChanges();


                return new User
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Token = accesToken,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public User RefreshToken(string refreshToken)
        {
            try
            {
                // Look for the refresh token in the database
                var storedToken = _chatAppContext.TblRefreshTokens
                    .SingleOrDefault(rt => rt.RefreshToken == refreshToken && rt.ExpiryDate > DateTime.UtcNow);

                if (storedToken == null)
                {
                    return null;  // Invalid or expired refresh token
                }

                var user = _chatAppContext.TblUsers.SingleOrDefault(u => u.Id == storedToken.UserId);
                if (user == null)
                {
                    return null;  // No user found for the refresh token
                }

                // Generate new access and refresh tokens
                var newAccessToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();

                // Update the refresh token in the database
                storedToken.RefreshToken = newRefreshToken;
                storedToken.ExpiryDate = DateTime.UtcNow.AddDays(7);  // Set new expiry date
                _chatAppContext.SaveChanges();

                return new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
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
            var entity = _chatAppContext.TblUsers
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (entity == null) return new User();

            return new User
            {
                UserName = entity.UserName,
                Id = entity.Id,
                AvatarSourceName = entity.AvatarSourceName,
                IsOnline = entity.IsOnline,
                LastLogonTime = entity.LastLogonTime
            };
        }
        private bool VerifityPassword(string enteredPassword, byte[] storedSalt, string storedPassword)
        {
            string encryptyedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            
            return encryptyedPassword.Equals(storedPassword);
        }
        private string GenerateJwtToken(TblUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("rweofkwurtihonmoiurwhbnrtgwrgjge");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("id", user.Id.ToString())]),
                Expires = DateTime.UtcNow.AddHours(12),             
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];  // 32 bytes = 256 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);  // Returns the random refresh token
        }

    }
}
