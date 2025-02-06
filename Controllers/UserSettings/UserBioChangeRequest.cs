namespace Voxerra_API.Controllers.UserSettings
{
    public class UserBioChangeRequest
    {
        public int UserId { get; set; }
        public string NewBio { get; set; } = null!;
    }
}