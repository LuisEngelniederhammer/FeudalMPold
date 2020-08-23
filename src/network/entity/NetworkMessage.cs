using System;
using Godot;

namespace FeudalMP.Network.Entity
{
	abstract public class NetworkMessage
	{
		protected NetworkMessageAction action;
		protected string data;
		protected SceneTree Tree;
	

		protected NetworkMessage(NetworkMessageAction action, string data)
		{
			this.action = action;
			this.data = data;
		}

		protected NetworkMessage(SceneTree Tree, String server)
		{
			this.Tree = Tree;
		}
		public abstract byte[] getPacket();

		public abstract void callback(int peerId, string data);
	}
}
