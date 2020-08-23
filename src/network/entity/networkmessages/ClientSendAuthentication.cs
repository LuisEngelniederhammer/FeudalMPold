using Godot;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientSendAuthentication : NetworkMessage
    {
        private string steamID64;
        public ClientSendAuthentication(string data) : base(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION, data)
        {
        }

        public ClientSendAuthentication(SceneTree Tree, string server) : base(Tree, server)
        {
        }

        public override void Callback(int peerId, NetworkMessage data)
        {
            throw new NotImplementedException();
        }

        public override byte[] GetPacket()
        {
            throw new NotImplementedException();
        }
    }
}
