using Godot;
using System;

using Newtonsoft.Json;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientPositionUpdate : NetworkMessage
    {
        public ClientPositionUpdate(Vector3 translation, Vector3 rotation) : base(NetworkMessageAction.CLIENT_POSITON_UPDATE, "")
        {
        }

        public ClientPositionUpdate(SceneTree Tree, string server) : base(Tree, server)
        {
        }

        public override void callback(int peerId, string data)
        {
            throw new NotImplementedException();
        }

        public override byte[] getPacket()
        {
            throw new NotImplementedException();
        }
    }
}
