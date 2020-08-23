using System;
using Godot;

namespace FeudalMP.Network.Entity
{
    abstract public class NetworkMessage
    {
        public NetworkMessageAction Action { get; }
        protected string data;
        protected SceneTree Tree;


        protected NetworkMessage(NetworkMessageAction action, string data)
        {
            this.Action = action;
            this.data = data;
        }

        protected NetworkMessage(SceneTree Tree, String server)
        {
            this.Tree = Tree;
        }
        public abstract byte[] GetPacket();

        public abstract void Callback(int peerId, NetworkMessage data);
    }
}
