using Godot;
using System;
using FeudalMP.Util;
using FeudalMP.Common;

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

        public override void callback(int peerId, string data)
        {
            Logger log = new Logger(this.GetType().Name);
            log.Info("new ServerInitialSync package received from server");
            //@TODO use received map file for loading
            new SceneService(Tree).LoadScene("DevWorld/DevWorld.scn");

            PackedScene charResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
            Node charNode = charResource.Instance();
            charNode.Name = GD.Str(Tree.GetNetworkUniqueId());
            charNode.SetNetworkMaster(Tree.GetNetworkUniqueId());
            Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(charNode);
        }

        public override byte[] getPacket()
        {
            JSON.Print(this);

            //tring json = JsonConvert.SerializeObject(this);
            string jsonString;
            //jsonString = JsonSerializer.Serialize(this);

            return new byte[1];
        }
    }
}
