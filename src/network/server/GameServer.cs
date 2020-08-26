using FeudalMP.Network.Entity;
using FeudalMP.Network.Entity.NetworkMessages;
using FeudalMP.Network.Server.Entity;
using FeudalMP.Util;
using Godot;

namespace FeudalMP.Network.Server
{
    public class GameServer : Object
    {
        private Logger LOG;
        private SceneTree Tree;
        private PacketDispatcher dispatcher;
        private NetworkedMultiplayerENet ENetInstance;
        public System.Collections.Generic.Dictionary<int, ClientRepresentation> ConnectedClients { get; set; }
        public GameServer(SceneTree Tree, int port)
        {
            LOG = new Logger(this.GetType().Name);
            ConnectedClients = new System.Collections.Generic.Dictionary<int, ClientRepresentation>();
            this.Tree = Tree;
            dispatcher = new PacketDispatcher(Tree);

            ENetInstance = new NetworkedMultiplayerENet();
            ENetInstance.CreateServer(port);
            Tree.NetworkPeer = ENetInstance;
            LOG.Info("Server started on port " + port);

            ENetInstance.Connect("peer_connected", this, "receive_peer_connected");
            ENetInstance.Connect("peer_disconnected", this, "receive_peer_disconnected");
            RegisterClientCallbacks();
        }

        public void receive_peer_connected(int id)
        {
            LOG.Warn("New client connected id=" + id);
        }
        public void receive_peer_disconnected(int id)
        {
            if (ConnectedClients.ContainsKey(id))
            {
                LOG.Info("Client disconnected id=" + id);
                RemoveClient(id);
                foreach (var entry in ConnectedClients)
                {
                    if (entry.Key != id)
                    {
                        ObjectBroker.Instance.NetworkService.toClient(entry.Key, new ClientPeerConnectionUpdate(new ClientRepresentation(id, new Vector3(), new Vector3()), true));
                    }
                }
            }
            else
            {
                LOG.Warn("Received peer disconnected signal from non registered client " + GD.Str(id));
            }

        }
        private void RegisterClientCallbacks()
        {
            //@TODO check if this is actually passed by reference and not by value
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION, new ClientSendAuthentication(Tree, this));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_POSITON_UPDATE, new ClientPositionUpdate(Tree, this));
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_CONNECTED_CLIENTS_SYNC, new ServerConnetedClientsSync(Tree, this));
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_COMPLETED_SYNC, new ServerCompletedSync(Tree, this));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_PEER_CONNECTION_UPDATE, new ClientPeerConnectionUpdate(Tree, this));
            dispatcher.RegisterCallback(NetworkMessageAction.CHAT_MESSAGE, new ChatMessage(Tree, this));
        }

        public void AddClient(ClientRepresentation client)
        {
            ConnectedClients.Add(client.Id, client);
        }

        public void RemoveClient(int id)
        {
            ConnectedClients.Remove(id);
        }
    }
}