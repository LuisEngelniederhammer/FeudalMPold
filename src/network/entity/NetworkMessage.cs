using System;
using Godot;

namespace FeudalMP.Network.Entity
{
	abstract public class NetworkMessage
	{
		private NetworkMessageAction action;
		private string data;

		protected NetworkMessage(NetworkMessageAction action, string data)
		{
			this.action = action;
			this.data = data;
		}

		protected NetworkMessage(SceneTree Tree, String server)
		{
		}
		public abstract byte[] getPacket();

		public abstract void callback(int peerId, string data);
	}
}
