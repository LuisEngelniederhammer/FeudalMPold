using FeudalMP.Network.Server;
using Godot;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
	public class ClientAnimationStateUpdate : AbstractNetworkMessage
	{
		public ClientAnimationStateUpdate(): base(NetworkMessageAction.CLIENT_ANIMATION_STATE_UPDATE){}
		public ClientAnimationStateUpdate(string animationName, bool backwards) : base(NetworkMessageAction.CLIENT_ANIMATION_STATE_UPDATE)
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
