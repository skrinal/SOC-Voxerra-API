﻿namespace Voxerra_API.Functions.UserFriend
{
    public interface IUserFriendFunction
    {
        Task<IEnumerable<User.User>> GetListUserFriend(int userId);
    }
}
