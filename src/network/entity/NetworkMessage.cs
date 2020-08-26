using FeudalMP.Network.Server;
using Godot;
using Godot.Collections;

namespace FeudalMP.Network.Entity
{
    public abstract class AbstractNetworkMessage
    {
        public NetworkMessageAction Action { get; set; }
        protected SceneTree Tree;
        protected GameServer Server;

        //this is Deprecated/only a default POCO Constructor
        public AbstractNetworkMessage() { }
        public AbstractNetworkMessage(NetworkMessageAction action)
        {
            this.Action = action;
        }

        public AbstractNetworkMessage(SceneTree Tree, GameServer Server)
        {
            this.Tree = Tree;
            this.Server = Server;
        }

        public abstract void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage);

        public abstract AbstractNetworkMessage Convert(string rawJson);
    }
}
