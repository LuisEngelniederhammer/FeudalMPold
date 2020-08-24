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
        private SceneTree Tree;
        private PacketDispatcher dispatcher;
        private NetworkService networkService;
        private NetworkedMultiplayerENet ENetInstance;

        public GameClient(SceneTree Tree, string ip, int port, string name)
        {
            LOG = new Logger(this.GetType().Name);
            this.Tree = Tree;
            dispatcher = new PacketDispatcher(Tree);
            RegisterClientCallbacks();
            ENetInstance = new NetworkedMultiplayerENet();
            ENetInstance.CreateClient(ip, port);
            Tree.NetworkPeer = ENetInstance;

            networkService = new NetworkService(Tree);

            ENetInstance.Connect("connection_failed", this, "receive_connection_failed");
            ENetInstance.Connect("connection_succeeded", this, "receive_connection_succeeded");

            PlayerName = name;

        }
        public void receive_connection_failed()
        {
            LOG.Warn("Cannot connect to server");
        }
        public void receive_connection_succeeded()
        {
            LOG.Info("Successfully connected to server");
            networkService.toServer(new ClientSendAuthentication(PlayerName), TransferModeEnum.Reliable);
        }

        private void RegisterClientCallbacks()
        {
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_INITIAL_SYNC, new ServerInitialSync(Tree, null));
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_CONNECTED_CLIENTS_SYNC, new ServerConnetedClientsSync(Tree, null));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_PEER_CONNECTION_UPDATE, new ClientPeerConnectionUpdate(Tree, null));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_POSITON_UPDATE, new ClientPositionUpdate(Tree, null));
        }
    }
}