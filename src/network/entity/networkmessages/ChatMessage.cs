using FeudalMP.Network.Server;
using FeudalMP.Network.Server.Entity;
using Godot;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ChatMessage : AbstractNetworkMessage
    {
        public string Message { get; set; }
        public ClientRepresentation Sender { get; set; }

        public ChatMessage() : base(NetworkMessageAction.CHAT_MESSAGE) { }
        public ChatMessage(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {

        }
        public ChatMessage(string Message, ClientRepresentation Sender) : base(NetworkMessageAction.CHAT_MESSAGE)
        {
            this.Message = Message;
            this.Sender = Sender;
        }
        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            ChatMessage receivedChatMessage = abstractNetworkMessage as ChatMessage;
            if (Tree.IsNetworkServer())
            {
                //ServerSide
                //TODO -> Check Message for command/ create command lexer

                //Add the sending peer as Sender on the server side, the client does not know about itself
                receivedChatMessage.Sender = Server.ConnectedClients[peerId];
                foreach (var entry in Server.ConnectedClients)
                {
                    //Send ChatMessage to every peer, even the sender himself
                    //Messages are only added by server message, not on sending on the client maschine
                    ObjectBroker.Instance.NetworkService.toClient(entry.Key, receivedChatMessage);
                }
            }
            else
            {
                //ClientSide
                if (ObjectBroker.Instance.SceneService.BaseUI.HasNode("ChatWindow"))
                {
                    Label newMessage = new Label();
                    newMessage.Autowrap = true;
                    newMessage.Text = String.Format("[{0}]: {1}", receivedChatMessage.Sender.Name, receivedChatMessage.Message);
                    ScrollContainer chatMessageContainer = ObjectBroker.Instance.SceneService.BaseUI.GetNode<ScrollContainer>("ChatWindow/ScrollContainer");
                    chatMessageContainer.GetNode("./VBoxContainer").AddChild(newMessage);
                    //chatMessageContainer.SetVScroll(chatMessageContainer.GetScroll)
                    chatMessageContainer.ScrollVertical = (int)chatMessageContainer.GetVScrollbar().MaxValue;
                }
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ChatMessage>(rawJson);
        }
    }
}
