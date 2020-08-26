using FeudalMP.Network.Server;
using FeudalMP.Network.Server.Entity;
using FeudalMP.Util;
using Godot;
using Newtonsoft.Json;
using System;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientPositionUpdate : AbstractNetworkMessage
    {
        private Logger LOG;

        public Vector3 Translation { get; set; }
        public Vector3 Rotation { get; set; }

        public ClientRepresentation ClientRepresentation { get; set; }

        public ClientPositionUpdate() : base(NetworkMessageAction.CLIENT_POSITON_UPDATE) { }
        //Client -> Server
        public ClientPositionUpdate(Vector3 translation, Vector3 rotation) : base(NetworkMessageAction.CLIENT_POSITON_UPDATE)
        {
            this.Translation = translation;
            this.Rotation = rotation;
        }

        public ClientPositionUpdate(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
            LOG = new Logger(this.GetType().Name);
        }

        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            ClientPositionUpdate clientPositionUpdate = abstractNetworkMessage as ClientPositionUpdate;
            if (Tree.IsNetworkServer())
            {
                //Update ClientRepresentation on server
                Server.ConnectedClients[peerId].Rotation = clientPositionUpdate.Rotation;
                Server.ConnectedClients[peerId].Translation = clientPositionUpdate.Translation;

                //send update to all connected clients
                foreach (int peer in Server.ConnectedClients.Keys)
                {
                    //do not send position update to the peer which has originally moved itself
                    //@TODO maybe change this later on for security/lokal hacking attempts
                    if (peer != peerId && Server.ConnectedClients[peerId].ClientState == ClientState.READY)
                    {
                        ClientPositionUpdate updateClients = new ClientPositionUpdate();
                        updateClients.ClientRepresentation = new ClientRepresentation(peerId, Server.ConnectedClients[peerId].Translation, Server.ConnectedClients[peerId].Rotation);
                        ObjectBroker.Instance.NetworkService.toClient(peer, updateClients);
                    }
                }
            }
            else
            {
                Spatial targetPeer = Tree.Root.GetNode("/root/FeudalMP/Scene").GetNode(GD.Str(clientPositionUpdate.ClientRepresentation.Id)) as Spatial;
                if (targetPeer != null)
                {
                    targetPeer.Translation = clientPositionUpdate.ClientRepresentation.Translation;
                    targetPeer.Rotation = clientPositionUpdate.ClientRepresentation.Rotation;
                }
                else
                {
                    LOG.Error("Trying to update non-existent targetPeer within scene");
                }
            }
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ClientPositionUpdate>(rawJson);
        }
    }
}
