using Godot;
using System;
using FeudalMP.Network.Entity;
using static Godot.NetworkedMultiplayerPeer;

namespace FeudalMP.Network
{
	public class NetworkService : Node
	{
		private SceneTree Tree;
		public NetworkService(SceneTree Tree)
		{
			this.Tree = Tree;
		}

		public Error toServerRaw(byte[] packet, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return Tree.Multiplayer.SendBytes(packet, TargetPeerServer, transferMode);
		}

		public Error toServer(NetworkMessage message, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return toServerRaw(message.getPacket(), transferMode);
		}

		public Error toClientRaw(int targetPeerId, byte[] packet, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return Tree.Multiplayer.SendBytes(packet, targetPeerId, transferMode);
		}

		public Error toClient(int targetPeerId, NetworkMessage message, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return toClientRaw(targetPeerId, message.getPacket(), transferMode);
		}
	}
}
