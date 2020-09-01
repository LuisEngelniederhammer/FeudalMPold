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
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            ClientPeerConnectionUpdate clientPeerConnectionUpdate = abstractNetworkMessage as ClientPeerConnectionUpdate;
            if (Tree.IsNetworkServer())
            {
                if (clientPeerConnectionUpdate.Disconnected)
                {
                    ClientRepresentation disconnectedPeer = Server.ConnectedClients[peerId];
                    //disconnect actual peer
                    Server.ENetInstance.DisconnectPeer(peerId, true);
                    foreach (var peer in Server.ConnectedClients)
                    {
                        if (peer.Key != peerId)
                        {
                            ObjectBroker.Instance.NetworkService.toClient(peer.Key, new ClientPeerConnectionUpdate(disconnectedPeer, true));
                        }
                    }
                }
            }
            else
            {
                if (!clientPeerConnectionUpdate.Disconnected)
                {
                    if (Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).HasNode(GD.Str(clientPeerConnectionUpdate.ClientRepresentation.Id)))
                    {
                        return;
                    }

                    PackedScene peerRepresentationResource = ResourceLoader.Load("res://assets/scenes/Character/Character.tscn") as PackedScene;
                    Godot.KinematicBody peerRepresentation = peerRepresentationResource.Instance() as Godot.KinematicBody;
                    peerRepresentation.Name = GD.Str(clientPeerConnectionUpdate.ClientRepresentation.Id);
                    peerRepresentation.SetNetworkMaster(clientPeerConnectionUpdate.ClientRepresentation.Id);
                    peerRepresentation.Translation = clientPeerConnectionUpdate.ClientRepresentation.Translation;
                    peerRepresentation.Rotation = clientPeerConnectionUpdate.ClientRepresentation.Rotation;

                    Node peerName = peerRepresentation.GetNode("Name");
                    ((Label)peerName.GetNode("Viewport/Label")).Text = clientPeerConnectionUpdate.ClientRepresentation.Name;

                    Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).AddChild(peerRepresentation);
                }
                else
                {
                    Node peerNode = Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).GetNode(GD.Str(clientPeerConnectionUpdate.ClientRepresentation.Id));
                    Tree.Root.GetNode(SceneService.NODE_PATH_SCENE).RemoveChild(peerNode);
                    peerNode.QueueFree();
                }
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ClientPeerConnectionUpdate>(rawJson);
        }
    }
}
