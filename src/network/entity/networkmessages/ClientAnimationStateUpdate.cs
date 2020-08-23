using Godot;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
	public class ClientAnimationStateUpdate : NetworkMessage
	{
		public ClientAnimationStateUpdate(string animationName, bool backwards) : base(NetworkMessageAction.CLIENT_POSITON_UPDATE, "")
		{
		}

		public ClientAnimationStateUpdate(SceneTree Tree, string server) : base(Tree, server)
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
