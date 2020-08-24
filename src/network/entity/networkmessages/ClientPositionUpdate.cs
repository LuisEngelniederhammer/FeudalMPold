using Godot;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientPositionUpdate : NetworkMessage
    {
        public ClientPositionUpdate(Vector3 translation, Vector3 rotation) : base(NetworkMessageAction.CLIENT_POSITON_UPDATE)
        {
        }

        public ClientPositionUpdate(SceneTree Tree, string server) : base(Tree, server)
        {
        }

        public override void Callback(int peerId, NetworkMessage data)
        {
            throw new NotImplementedException();
        }

    }
}
