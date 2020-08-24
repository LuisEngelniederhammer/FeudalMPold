using Godot;
using System;
using FeudalMP.Util;
using FeudalMP.Common;
using FeudalMP.Network.Server;
using Newtonsoft.Json;
using FeudalMP.Network.Server.Entity;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientPeerConnectionUpdate : AbstractNetworkMessage
    {
        private NetworkService networkService;
        public ClientRepresentation ClientRepresentation { get; set; }
        public bool Disconnected { get; set; }

        public ClientPeerConnectionUpdate() : base(NetworkMessageAction.CLIENT_PEER_CONNECTION_UPDATE) { }
        public ClientPeerConnectionUpdate(ClientRepresentation clientRepresentation, bool disconnected = false) : base(NetworkMessageAction.CLIENT_PEER_CONNECTION_UPDATE)
        {
            this.ClientRepresentation = clientRepresentation;
            this.Disconnected = disconnected;
        }

        public ClientPeerConnectionUpdate(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
            networkService = new NetworkService(Tree);
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            if (Tree.IsNetworkServer())
            {
            }
            else
            {
                ClientPeerConnectionUpdate clientPeerConnectionUpdate = abstractNetworkMessage as ClientPeerConnectionUpdate;
                if (!Disconnected)
                {
                    PackedScene peerRepresentationResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
                    Godot.KinematicBody peerRepresentation = peerRepresentationResource.Instance() as Godot.KinematicBody;
                    peerRepresentation.Name = GD.Str(clientPeerConnectionUpdate.ClientRepresentation.Id);
                    peerRepresentation.SetNetworkMaster(clientPeerConnectionUpdate.ClientRepresentation.Id);
                    peerRepresentation.Translation = clientPeerConnectionUpdate.ClientRepresentation.Translation;
                    peerRepresentation.Rotation = clientPeerConnectionUpdate.ClientRepresentation.Rotation;
                    Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(peerRepresentation);
                }
                else
                {
                    Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).GetNode(GD.Str(clientPeerConnectionUpdate.ClientRepresentation.Id)).QueueFree();
                }
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ClientPeerConnectionUpdate>(rawJson);
        }
    }
}
