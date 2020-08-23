using Godot;
using System;
using FeudalMP.Util;
using FeudalMP.Common;
using System.Text.Json;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerInitialSync : NetworkMessage
    {
        public string MapFilePath { get; set; }
        public ServerInitialSync(string data) : base(NetworkMessageAction.SERVER_INITIAL_SYNC, data)
        {
            MapFilePath = data;
        }

        public ServerInitialSync(SceneTree Tree, string server) : base(Tree, server)
        {
        }

        public override void Callback(int peerId, NetworkMessage data)
        {
            Logger log = new Logger(this.GetType().Name);
            log.Info("new ServerInitialSync package received from server");
            ServerInitialSync received = data as ServerInitialSync;
            new SceneService(Tree).LoadScene(received.MapFilePath);

            PackedScene charResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
            Node charNode = charResource.Instance();
            charNode.Name = GD.Str(Tree.GetNetworkUniqueId());
            charNode.SetNetworkMaster(Tree.GetNetworkUniqueId());
            Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(charNode);
        }

        public override byte[] GetPacket()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this);
        }
    }
}
