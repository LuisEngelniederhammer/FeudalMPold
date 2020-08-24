using Godot;
using FeudalMP.Network.Entity;
using static Godot.NetworkedMultiplayerPeer;
using Newtonsoft.Json;

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

		public Error toServer(AbstractNetworkMessage message, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return toServerRaw(JsonConvert.SerializeObject(message).ToUTF8(), transferMode);
		}

		public Error toClientRaw(int targetPeerId, byte[] packet, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return Tree.Multiplayer.SendBytes(packet, targetPeerId, transferMode);
		}

		public Error toClient(int targetPeerId, AbstractNetworkMessage message, TransferModeEnum transferMode = TransferModeEnum.Unreliable)
		{
			return toClientRaw(targetPeerId, JsonConvert.SerializeObject(message).ToUTF8(), transferMode);
		}
	}
}
