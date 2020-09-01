using Godot;
using FeudalMP.Network.Server;
using Newtonsoft.Json;
using FeudalMP.Network.Server.Entity;
using static Godot.NetworkedMultiplayerPeer;
using FeudalMP.Util;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerCompletedSync : AbstractNetworkMessage
    {
        private Logger logger;
        public ServerCompletedSync() : base(NetworkMessageAction.SERVER_COMPLETED_SYNC)
        {

        }

        public ServerCompletedSync(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
            logger = new Logger(this.GetType().Name);
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            if (Tree.IsNetworkServer())
            {
                if(Server.ConnectedClients[peerId].ClientState == ClientState.READY){
                    return;
                }
                Server.ConnectedClients[peerId].ClientState = ClientState.READY;
                logger.Info("there are " + GD.Str(Server.ConnectedClients.Count) + " connected clients, the will all updated with " + GD.Str(peerId));
                foreach (var peer in Server.ConnectedClients)
                {
                    if (peer.Key != peerId)
                    {
                        
                        ObjectBroker.Instance.NetworkService.toClient(peer.Key, new ClientPeerConnectionUpdate(Server.ConnectedClients[peerId]), TransferModeEnum.Reliable);
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
