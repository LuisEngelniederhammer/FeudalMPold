using FeudalMP.Network.Entity;
using FeudalMP.Network.Entity.NetworkMessages;
using FeudalMP.Util;
using Godot;
using static Godot.NetworkedMultiplayerPeer;

namespace FeudalMP.Network.Client
{
    public class GameClient : Node
    {
        private Logger LOG;
        private string PlayerName;
        private PacketDispatcher dispatcher;
        private NetworkedMultiplayerENet ENetInstance;

        public override void _Ready()
        {
            LOG = new Logger(this.GetType().Name);
            dispatcher = new PacketDispatcher(GetTree());
            
            PlayerName = GetNode<Godot.LineEdit>("/root/FeudalMP/UI/Control/HBoxContainer/CenterContainer/VBoxContainer/name/LineEdit").Text;
            string ip = GetNode<Godot.LineEdit>("/root/FeudalMP/UI/Control/HBoxContainer/CenterContainer/VBoxContainer/ip/LineEdit").Text;
            int port = System.Int32.Parse(GetNode<Godot.LineEdit>("/root/FeudalMP/UI/Control/HBoxContainer/CenterContainer/VBoxContainer/port/LineEdit").Text);

            RegisterClientCallbacks();
            ENetInstance = new NetworkedMultiplayerENet();
            ENetInstance.CreateClient(ip, port);
            GetTree().NetworkPeer = ENetInstance;

            ENetInstance.Connect("connection_failed", this, "receive_connection_failed");
            ENetInstance.Connect("connection_succeeded", this, "receive_connection_succeeded");
            ENetInstance.Connect("server_disconnected", this, "receive_server_disconnected");
        }
        public void receive_server_disconnected()
        {
            LOG.Warn("Connection to server was lost");
            GetTree().NetworkPeer = null;
            ENetInstance = null;
            dispatcher = null;
            ObjectBroker.Instance.SceneService.LoadUI("MainMenu/MainMenu.tscn");
        }
        public void receive_connection_failed()
        {
            LOG.Warn("Cannot connect to server");
        }
        public void receive_connection_succeeded()
        {
            LOG.Info("Successfully connected to server");
            ObjectBroker.Instance.NetworkService.toServer(new ClientSendAuthentication(PlayerName), TransferModeEnum.Reliable);
        }

        private void RegisterClientCallbacks()
        {
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_INITIAL_SYNC, new ServerInitialSync(GetTree(), null));
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_CONNECTED_CLIENTS_SYNC, new ServerConnetedClientsSync(GetTree(), null));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_PEER_CONNECTION_UPDATE, new ClientPeerConnectionUpdate(GetTree(), null));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_POSITON_UPDATE, new ClientPositionUpdate(GetTree(), null));
            dispatcher.RegisterCallback(NetworkMessageAction.CHAT_MESSAGE, new ChatMessage(GetTree(), null));
        }

        public override void _ExitTree(){
            dispatcher = null;
        }
    }
}
