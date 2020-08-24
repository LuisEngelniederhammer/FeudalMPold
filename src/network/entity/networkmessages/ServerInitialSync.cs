using Godot;
using System;
using FeudalMP.Util;
using FeudalMP.Common;
using FeudalMP.Network.Server;
using Newtonsoft.Json;
using FeudalMP.Network.Server.Entity;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerInitialSync : AbstractNetworkMessage
    {
        public String MapFilePath { get; set; }
        public ServerInitialSync() { }
        public ServerInitialSync(String MapFilePath) : base(NetworkMessageAction.SERVER_INITIAL_SYNC)
        {
            this.MapFilePath = MapFilePath;
        }

        public ServerInitialSync(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            ServerInitialSync s = abstractNetworkMessage as ServerInitialSync;
            Logger log = new Logger(this.GetType().Name);
            log.Info("new ServerInitialSync package received from server");
            SceneService sceneService = ((SceneService)Tree.Root.GetNode("/root/SceneService"));
            sceneService.LoadSceneDeferred("res://assets/scenes/" + s.MapFilePath);

            PackedScene charResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
            Node charNode = charResource.Instance();
            charNode.Name = GD.Str(Tree.GetNetworkUniqueId());
            charNode.SetNetworkMaster(Tree.GetNetworkUniqueId());
            Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(charNode);
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ServerInitialSync>(rawJson);
        }
    }
}
