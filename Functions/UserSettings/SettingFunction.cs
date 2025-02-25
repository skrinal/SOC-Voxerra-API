using Voxerra_API.Functions.Registration;

namespace Voxerra_API.Functions.Settings;

public class SettingFunction(ChatAppContext chatAppContext,IUserRegistrationFunction registrationFunction) : ISettingFunction
{
    private ChatAppContext _chatAppContext = chatAppContext;
    private IUserRegistrationFunction _registrationFunction = registrationFunction;
    
    public async Task<bool> ChangeUserName(int userId, string newUserName)
    {
        try
        {
            await _chatAppContext.Tblusers
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(u => u.UserName, newUserName));

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<string> ReturnEmail(int userId)
    {
        try
        {
            var email = await _chatAppContext.Tblusers
                .Where(x => x.Id == userId)
                .Select(x => x.Email)
                .FirstOrDefaultAsync();

            return email;
        }
        catch (Exception e)
        {
            return "";
        }
    } 
    
    public async Task<bool> ChangeEmail(int userId, string newEmail)
    {
        // musi poslat code na novy email az potom ho zmenit
        try
        {
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == userId);
            
            user.Email = newEmail;
            await _chatAppContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<bool> ChangeBio(int userId, string newBio)
    {
        try
        {
            await _chatAppContext.Tblusers
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(u => u.Bio, newBio));

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ChangePassword(int userId, string newPassword)
    {
        try
        {
            var (encryptedPass, salt) = _registrationFunction.EncryptPassword(newPassword);

            await _chatAppContext.Tblusers
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.Password, encryptedPass)
                    .SetProperty(u => u.StoredSalt, salt));

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> DeleteAccount(int userId)
    {
        try
        {
            var user = await _chatAppContext.Tblusers
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) return false;

            _chatAppContext.Tblusers.Remove(user);
            _chatAppContext.Tbluserfriends.RemoveRange(user.Friends);
            /*
            _chatAppContext.Tbluserfriends.RemoveRange(
                await _chatAppContext.Tbluserfriends
                    .Where(x => x.UserId == userId || x.FriendId == userId)
                    .ToListAsync()
            );
            */

            await _chatAppContext.SaveChangesAsync();

            // zmazat folder s upload

            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }


    public async Task<bool> TwoAuthUpdate(int userId, bool decision)
    {
        try
        {
            await _chatAppContext.Tblusersettings
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(u => u.TwoFactorEnabled, decision));

            return true;
        }   
        catch (Exception){ return false; } 
    }
}