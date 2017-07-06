using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
namespace MultiClientChatDemo.Web
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        static int _counter = 0;
        public void SendMessage(string sender, string msg, string reciever)
        {
            Clients.Others.stopThatTypingThing();
            Clients.Others.receiveMessage(sender, msg, reciever);
            Clients.Caller.meraMessage(sender, msg);            
        }

        public void IsThisPersonTyping(string sender)
        {
            Clients.Others.getTypist(sender);
        }

        public void NotTyping()
        {
            Clients.Others.stopThatTypingThing();
        }

        public void record(string peronName)
        {
            _counter++;
            Clients.All.receiveHit(_counter);
            Clients.All.updateUserList(peronName, "entered the room");
        }

        public void disconnectedFromServer(string sender)
        {
            _counter--;
            Clients.All.stopThatTypingThing();
            Clients.All.okieByeBye(sender, "left the room");
            Clients.All.receiveHit(_counter);
        }
    }
}