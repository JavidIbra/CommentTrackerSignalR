using Microsoft.AspNetCore.SignalR;

namespace CommentTrackerTest.Hubs
{
    public class TestHub : Hub
    {
        public async Task SendAsync() 
        {
          await Clients.All.SendAsync("ReceiveMessage", "salam");
        }
    }
}
