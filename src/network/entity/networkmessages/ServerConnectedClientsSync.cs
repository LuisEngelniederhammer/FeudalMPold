using System.Collections.Generic;
using FeudalMP.Common;
using FeudalMP.Network.Server;
using FeudalMP.Util;
using Godot;
using Newtonsoft.Json;
using static Godot.NetworkedMultiplayerPeer;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerConnetedClientsSync : AbstractNetworkMessage
    {
        private NetworkService networkService;
        public Dictionary<int, Vector3> ConnectedClients { get; set; }
        private Logger LOG;
        public ServerConnetedClientsSync() : base(NetworkMessageAction.SERVER_CONNECTED_CLIENTS_SYNC) { }

        public ServerConnetedClientsSync(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
            networkService = new NetworkService(Tree);
            LOG = new Logger(this.GetType().Name);
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            if (Tree.IsNetworkServer())
            {
                ConnectedClients = new Dictionary<int, Vector3>();
                foreach (var entry in Server.ConnectedClients)
                {
                    if (entry.Key != peerId)
                    {
                        ConnectedClients.Add(entry.Key, entry.Value.Translation);
                    }
                }
                this.Action = NetworkMessageAction.SERVER_CONNECTED_CLIENTS_SYNC;
                networkService.toClient(peerId, this, TransferModeEnum.Reliable);
            }
            else
            {
                ServerConnetedClientsSync serverConnetedClientsSync = abstractNetworkMessage as ServerConnetedClientsSync;
                foreach (var entry in serverConnetedClientsSync.ConnectedClients)
                {
                    if (Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).HasNode(GD.Str(entry.Key)))
                    {
                        LOG.Warn(System.String.Format("Node with name {0} is already present in the current client scene", entry.Key));
                    }

                    PackedScene peerRepresentationResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
                    Godot.KinematicBody peerRepresentation = peerRepresentationResource.Instance() as Godot.KinematicBody;
                    peerRepresentation.Name = GD.Str(entry.Key);
                    peerRepresentation.SetNetworkMaster(entry.Key);
                    peerRepresentation.Translation = entry.Value;
                    Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(peerRepresentation);
                }
                networkService.toServer(new ServerCompletedSync(),TransferModeEnum.Reliable);
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ServerConnetedClientsSync>(rawJson);
        }
    }
}