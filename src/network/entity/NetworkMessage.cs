using System;
using Godot;
using Godot.Collections;

namespace FeudalMP.Network.Entity
{
    public class NetworkMessage : Node
    {
        public NetworkMessageAction Action { get; set; }
        public Dictionary Data { get; set; }
        protected SceneTree Tree;

        //this is Deprecated/only a default POCO Constructor
        public NetworkMessage() { Data = new Dictionary(); }
        protected NetworkMessage(NetworkMessageAction action)
        {
            this.Action = action;
            Data = new Dictionary();
        }

        protected NetworkMessage(SceneTree Tree, String server)
        {
            this.Tree = Tree;
        }

        public virtual void Callback(int peerId, NetworkMessage data) { }

        public Godot.Collections.Dictionary getPacket()
        {
            Dictionary d = new Dictionary();
            d.Add("Action", Action);
            d.Add("Data", Data);
            return d;

        }
    }
}
