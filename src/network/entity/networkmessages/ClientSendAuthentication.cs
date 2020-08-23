using FeudalMP.Util;
using Godot;
using System;
using static Godot.NetworkedMultiplayerPeer;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientSendAuthentication : NetworkMessage
    {
        private string steamID64;
        private NetworkService networkService;
        public ClientSendAuthentication(string data) : base(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION, data)
        {
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

        public override byte[] GetPacket()
        {
            throw new NotImplementedException();
        }
    }
}
