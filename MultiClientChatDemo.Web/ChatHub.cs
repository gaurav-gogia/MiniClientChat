using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MultiClientChatDemo.Web
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        static int _counter = 0;       
        public void SendMessage(string sender, string msg)
        {
            Clients.Others.stopThatTypingThing();

            Clients.Others.receiveMessage(sender, msg); // iske liye left wali list            
            Clients.Caller.meraMessage(sender, msg); // iske liye right wali list            

            Clients.Others.buzzer();
        }

        public void IsThisPersonTyping(string sender)
        {
            Clients.Others.getTypist(sender);
        }


        public void NotTyping()
        {
            Clients.Others.stopThatTypingThing();
        }

        public void record()
        {         
            _counter++;            
            Clients.All.receiveHit(_counter);
        }        

        public void disconnectedFromServer(string sender)
        {
            _counter--;
            Clients.All.stopThatTypingThing();
            Clients.All.okieByeBye(sender, "left conversation room");
            Clients.All.receiveHit(_counter);
        }
    }
}