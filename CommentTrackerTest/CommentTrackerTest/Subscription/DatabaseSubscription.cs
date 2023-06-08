using CommentTrackerTest.Hubs;
using Microsoft.AspNetCore.SignalR;
using TableDependency.SqlClient;

namespace CommentTrackerTest.Subscription
{
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class,new()
    {
        IConfiguration _configuration;
        IHubContext<TestHub> _hubContext;

        public DatabaseSubscription(IConfiguration configuration, IHubContext<TestHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        SqlTableDependency<T> _sqlTableDependency; 

        public void Subscribe(string tableName)
        {
            _sqlTableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("MSSQL"), tableName);

            _sqlTableDependency.OnChanged += async (e, o) =>
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "salam");
            };

            _sqlTableDependency.OnError += (e, o) =>
            {

            };

            _sqlTableDependency.Start();
        }

        ~DatabaseSubscription() 
        {
            _sqlTableDependency.Stop();
        }
    }
}
