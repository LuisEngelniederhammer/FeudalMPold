using System;
using Godot;

namespace FeudalMP.Network.Entity
{
	public enum NetworkMessageAction
	{
		CLIENT_SEND_AUTHENTICATION,
		CLIENT_POSITON_UPDATE,
		CLIENT_ANIMATION_STATE_UPDATE,

		SERVER_INITIAL_SYNC
	}
}
