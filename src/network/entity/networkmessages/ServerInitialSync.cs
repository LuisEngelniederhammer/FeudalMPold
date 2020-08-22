using Godot;
using System;

using Newtonsoft.Json;

namespace FeudalMP.Network.Entity.NetworkMessages
{
	public class ServerInitialSync : NetworkMessage
	{
		private string mapFilePath;
		public ServerInitialSync(string data) : base(NetworkMessageAction.SERVER_INITIAL_SYNC, data)
		{
		}

		public ServerInitialSync(SceneTree Tree, string server) : base(Tree, server)
		{
		}

		public override void callback(int peerId, string data)
		{
			throw new NotImplementedException();
		}

		public override byte[] getPacket()
		{
			JSON.Print(this);
			string json = JsonConvert.SerializeObject(this);

			return new byte[1];
		}
	}
}
