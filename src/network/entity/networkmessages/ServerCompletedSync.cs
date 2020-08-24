using Godot;
using FeudalMP.Network.Server;
using Newtonsoft.Json;
using FeudalMP.Network.Server.Entity;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerCompletedSync : AbstractNetworkMessage
    {
        private NetworkService networkService;
        public ServerCompletedSync() : base(NetworkMessageAction.SERVER_COMPLETED_SYNC)
        {

        }

        public ServerCompletedSync(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
            networkService = new NetworkService(Tree);
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            if (Tree.IsNetworkServer())
            {
                Server.ConnectedClients[peerId].ClientState = ClientState.READY;
                foreach (var peer in Server.ConnectedClients)
                {
                    if (peer.Key != peerId)
                    {
                        networkService.toClient(peer.Key, new ClientPeerConnectionUpdate(Server.ConnectedClients[peerId]));
                    }
                }
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ServerCompletedSync>(rawJson);
        }
    }
}
