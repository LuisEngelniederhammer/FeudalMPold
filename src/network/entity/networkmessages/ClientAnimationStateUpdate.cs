using FeudalMP.Network.Server;
using Godot;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
	public class ClientAnimationStateUpdate : AbstractNetworkMessage
	{
		public ClientAnimationStateUpdate(){}
		public ClientAnimationStateUpdate(string animationName, bool backwards) : base(NetworkMessageAction.CLIENT_POSITON_UPDATE)
		{
		}

		public ClientAnimationStateUpdate(SceneTree Tree, GameServer Server) : base(Tree, Server)
		{
		}

		public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
		{
			throw new NotImplementedException();
		}

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            throw new NotImplementedException();
        }
    }
}
