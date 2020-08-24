using System.Collections.Generic;
using Godot;

namespace FeudalMP.Network.Entity
{
    public class NetworkMessagePacket
    {
        public NetworkMessageAction Action { get; set; }
        public Dictionary<object, object> Data { get; set; }
        public NetworkMessagePacket() { }
        public NetworkMessagePacket(NetworkMessageAction Action, Dictionary<object, object> Data) { 
            this.Action = Action;
            this.Data = Data;
        }
    }
}