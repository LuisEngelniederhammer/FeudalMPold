using FeudalMP.Network.Entity;
using FeudalMP.Network.Entity.NetworkMessages;
using FeudalMP.Network.Server.Entity;
using FeudalMP.Util;
using Godot;

namespace FeudalMP.Network.Server
{
    public class GameServer : Godot.Node
    {
        private int port;
        private Logger LOG;
        private PacketDispatcher dispatcher;
        public NetworkedMultiplayerENet ENetInstance { get; private set; }
        public System.Collections.Generic.Dictionary<int, ClientRepresentation> ConnectedClients { get; set; }
        public override void _Ready()
        {
            LOG = new Logger(this.GetType().Name);
            ConnectedClients = new System.Collections.Generic.Dictionary<int, ClientRepresentation>();
            PackedScene dispatcherPackedScene = GD.Load("res://src/network/PacketDispatcher.tscn") as PackedScene;
            dispatcher = (PacketDispatcher)dispatcherPackedScene.Instance();
            AddChild(dispatcher);

            RegisterServerCallbacks();
            port =(int)ProjectSettings.GetSetting("feudal_mp/server/port");
            ENetInstance = new NetworkedMultiplayerENet();
            ENetInstance.CreateServer(port);
            GetTree().NetworkPeer = ENetInstance;
            LOG.Info("Server started on port " + port);

            ENetInstance.Connect("peer_connected", this, "receive_peer_connected");
            ENetInstance.Connect("peer_disconnected", this, "receive_peer_disconnected");
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
        private void RegisterServerCallbacks()
        {
            //@TODO check if this is actually passed by reference and not by value
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION, new ClientSendAuthentication(GetTree(), this));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_POSITON_UPDATE, new ClientPositionUpdate(GetTree(), this));
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_CONNECTED_CLIENTS_SYNC, new ServerConnetedClientsSync(GetTree(), this));
            dispatcher.RegisterCallback(NetworkMessageAction.SERVER_COMPLETED_SYNC, new ServerCompletedSync(GetTree(), this));
            dispatcher.RegisterCallback(NetworkMessageAction.CLIENT_PEER_CONNECTION_UPDATE, new ClientPeerConnectionUpdate(GetTree(), this));
            dispatcher.RegisterCallback(NetworkMessageAction.CHAT_MESSAGE, new ChatMessage(GetTree(), this));
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
