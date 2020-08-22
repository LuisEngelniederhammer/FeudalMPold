using System;
using Godot;

namespace FeudalMP.Network.Entity
{
	abstract public class NetworkMessage : Node
	{
		private int action;
		private string data;

		protected NetworkMessage(int action, String data)
		{
			this.action = action;
			this.data = data;
		}

	}
}
