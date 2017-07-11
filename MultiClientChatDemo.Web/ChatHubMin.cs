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
            Clients.Caller.meraMessage(sender, msg, reciever);            
        }

        public void YesTyping(string sender)
        {
            Clients.Others.getTypist(sender);
        }

        public void NotTyping()
        {
            Clients.All.typingStopped();
        }

        public void record(string peronName)
        {
            _counter++;
            Clients.All.receiveHit(_counter);
            Clients.All.updateUserList(peronName, "entered the room");

            int[] iv = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int padding = 5;

            Clients.All.getIVPadding(iv, padding);
        }

        public void disconnectedFromServer(string sender)
        {
            _counter--;            
            Clients.All.okieByeBye(sender, "left the room");
            Clients.All.receiveHit(_counter);
        }        
    }
}