using Godot;
using System;
using FeudalMP.Util;
using FeudalMP.Common;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerInitialSync : NetworkMessage
    {
        public ServerInitialSync(String MapFilePath) : base(NetworkMessageAction.SERVER_INITIAL_SYNC)
        {
            Data.Add("MapFilePath", MapFilePath);
        }

        public ServerInitialSync(SceneTree Tree, string server) : base(Tree, server)
        {
        }

        public override void Callback(int peerId, NetworkMessage received)
        {
            Logger log = new Logger(this.GetType().Name);
            log.Info("new ServerInitialSync package received from server");
            SceneService sceneService = ((SceneService)Tree.Root.GetNode("/root/SceneService"));
            sceneService.LoadSceneDeferred("res://assets/scenes/" + received.Data["MapFilePath"] as String);

            PackedScene charResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
            Node charNode = charResource.Instance();
            charNode.Name = GD.Str(Tree.GetNetworkUniqueId());
            charNode.SetNetworkMaster(Tree.GetNetworkUniqueId());
            Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(charNode);
        }
    }
}
