using FeudalMP.Util;
using Godot;
using static Godot.NetworkedMultiplayerPeer;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientSendAuthentication : NetworkMessage
    {
        private NetworkService networkService;
        public ClientSendAuthentication(string steamID64) : base(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION)
        {
            Data.Add("steamID64", steamID64);
        }

        public ClientSendAuthentication(SceneTree Tree, string server) : base(Tree, server)
        {
            networkService = new NetworkService(Tree);
        }

        public override void Callback(int peerId, NetworkMessage data)
        {
            Logger LOG = new Logger(this.GetType().Name);
            LOG.Info("Received callback");
            networkService.toClient(peerId, new ServerInitialSync("DevWorld/DevWorld.scn"), TransferModeEnum.Reliable);
        }
    }
}
