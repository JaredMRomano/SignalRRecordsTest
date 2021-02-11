using Microsoft.AspNetCore.SignalR;
using SignalRRecordsTest.Requests;
using System.Threading.Tasks;

namespace SignalRRecordsTest.Hubs
{
    public class RecordsTestHub : Hub
    {
        public async Task SendStandardClassRequest(StandardClassRequest request)
        {
            await Clients
                .Caller
                .SendAsync("OnMessageRecieved", request.StringProp);
        }

        public async Task SendRecordRequest(PositionalRecordRequest request)
        {
            await Clients
                .Caller
                .SendAsync("OnMessageRecieved", request.StringProp);
        }

        public async Task SendImmutableClassRequest(ImmutableClassRequest request)
        {
            await Clients
                .Caller
                .SendAsync("OnMessageRecieved", request.StringProp);
        }
    }
}
