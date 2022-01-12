using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace webapi.Communication
{
    public class HubService
    {
        private readonly IHubContext<MessageHub> hubContext;

        public HubService(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task  NotifyOnGameChanges(int gameID, string method, Object object_to_send)
        {
            await hubContext.Clients.Group("Game" + gameID).SendAsync(method, object_to_send);
        }
    }
}