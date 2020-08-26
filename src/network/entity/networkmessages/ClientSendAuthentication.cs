using FeudalMP.Network.Server;
using FeudalMP.Network.Server.Entity;
using FeudalMP.Util;
using Godot;
using Newtonsoft.Json;
using static Godot.NetworkedMultiplayerPeer;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class ClientSendAuthentication : AbstractNetworkMessage
    {
        public System.String SteamID64 { get; set; }
        public ClientSendAuthentication() : base(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION){ }
        //Called by client
        public ClientSendAuthentication(string steamID64) : base(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION)
        {
            this.SteamID64 = steamID64;
        }

        //Registering on the server side CallbackRegister of the PacketDispatcher
        public ClientSendAuthentication(SceneTree Tree, GameServer Server) : base(Tree, Server)
        {
        }

        //Called when received by server
        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            ClientSendAuthentication clientSendAuthentication = abstractNetworkMessage as ClientSendAuthentication;
            //GD.Print(c);
            Logger LOG = new Logger(this.GetType().Name);
            LOG.Info(System.String.Format("Adding client {0} with name {1} to client list. Sending map data to client", peerId, clientSendAuthentication.SteamID64));
            ClientRepresentation newClient = new ClientRepresentation(peerId, new Vector3(), new Vector3());
            newClient.Name = clientSendAuthentication.SteamID64;
            Server.AddClient(newClient);
            ObjectBroker.Instance.NetworkService.toClient(peerId, new ServerInitialSync("DevWorld/DevWorld.scn"), TransferModeEnum.Reliable);
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<ClientSendAuthentication>(rawJson);
        }
    }
}
