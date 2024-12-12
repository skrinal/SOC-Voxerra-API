
namespace Voxerra_API.Controllers.ChatHub
{
    public class ChatHub : Hub
    {
        UserOperator _userOperator;
        IMessageFunction _messageFunction;
        private static readonly Dictionary<int, string> _connectionMapping
            = new Dictionary<int, string>();
        public ChatHub(UserOperator userOperator, IMessageFunction messageFunction)
        {
            _userOperator = userOperator;
            _messageFunction = messageFunction;  
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToUser(int fromUserId, int toUserId, string message)
        {
            var connectionIds = _connectionMapping.Where(x => x.Key == toUserId)
                                                        .Select(x => x.Value).ToList();

            await _messageFunction.AddMessage(fromUserId, toUserId, message);

            await Clients.Clients(connectionIds)
                .SendAsync("ReceiveMessage", fromUserId, message);

        }

        //public override Task OnConnectedAsync()
        //{
        //    var userId = _userOperator.GetRequestUser().Id;
        //    if (!_connectionMapping.ContainsKey(userId))
        //        _connectionMapping.Add(userId, Context.ConnectionId);

        //    return base.OnConnectedAsync();
        //}

        public override async Task OnConnectedAsync()
        {
            // Check if _userOperator is null
            if (_userOperator == null)
            {
                throw new InvalidOperationException("User  operator is not initialized.");
            }

            // Get the user
            var user = _userOperator.GetRequestUser();

            // Check if user is null
            if (user == null)
            {
                throw new InvalidOperationException("User  is not found.");
            }

            var userId = user.Id;

            // Check if _connectionMapping is null
            if (_connectionMapping == null)
            {
                throw new InvalidOperationException("Connection mapping is not initialized.");
            }

            // Add connection ID to the mapping
            if (!_connectionMapping.ContainsKey(userId))
            {
                _connectionMapping.Add(userId, Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connectionMapping.Remove(_userOperator.GetRequestUser().Id);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
