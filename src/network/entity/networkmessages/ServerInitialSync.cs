using Godot;
using System;
using FeudalMP.Util;
using FeudalMP.Common;
using FeudalMP.Network.Server;
using Newtonsoft.Json;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ServerInitialSync : AbstractNetworkMessage
    {
        public String MapFilePath { get; set; }
        public ServerInitialSync() : base(NetworkMessageAction.SERVER_INITIAL_SYNC) { }
        public ServerInitialSync(String MapFilePath) : base(NetworkMessageAction.SERVER_INITIAL_SYNC)
        {
            this.MapFilePath = MapFilePath;
        }

        public ServerInitialSync(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            if (!Tree.IsNetworkServer())
            {
                //Client side code
                ServerInitialSync s = abstractNetworkMessage as ServerInitialSync;
                Logger log = new Logger(this.GetType().Name);
                log.Info("new ServerInitialSync package received from server");
                FeudalMP.ObjectBroker.Instance.SceneService.LoadSceneDeferred("res://assets/scenes/" + s.MapFilePath);

                FeudalMP.ObjectBroker.Instance.SceneService.AttachUI("ChatWindow/ChatWindow.tscn");

                PackedScene charResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
                Node charNode = charResource.Instance();
                charNode.Name = GD.Str(Tree.GetNetworkUniqueId());
                charNode.SetNetworkMaster(Tree.GetNetworkUniqueId());
                Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(charNode);
                ObjectBroker.Instance.NetworkService.toServer(new ServerConnetedClientsSync());
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ServerInitialSync>(rawJson);
        }
    }
}
