using Voxerra_API.Functions.Registration;

namespace Voxerra_API.Functions.Settings;

public class SettingFunction(ChatAppContext chatAppContext) : ISettingFunction
{
    private ChatAppContext _chatAppContext = chatAppContext;
    
    public async Task<bool> ChangeUserName(int userId, string newUserName)
    {
        try
        {
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == userId);

            user.UserName = newUserName;
            await _chatAppContext.SaveChangesAsync();

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
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == userId);
            return user.Email;
        }
        catch (Exception e)
        {
            return "";
        }
    } 
    
    public async Task<bool> ChangeEmail(int userId, string newEmail)
    {
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
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == userId);
            
            user.Bio = newBio;
            await _chatAppContext.SaveChangesAsync();

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
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == userId);
            
            user.Password = newPassword;
            await _chatAppContext.SaveChangesAsync();

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
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;

            _chatAppContext.Tblusers.Remove(user);
            
            var friends = await _chatAppContext.Tbluserfriends
                .Where(x => (x.UserId == userId ||x.FriendId == userId ))
                .ToListAsync();
            
            if (friends.Any()) _chatAppContext.Tbluserfriends.RemoveRange(friends);
                
            await _chatAppContext.SaveChangesAsync(); 

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
            var user = await _chatAppContext.Tblusersettings
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if( user == null) return false;

            user.TwoFactorEnabled = decision;
            await _chatAppContext.SaveChangesAsync();

            return true;
        }   
        catch (Exception ex){ return false; } 
    }
}