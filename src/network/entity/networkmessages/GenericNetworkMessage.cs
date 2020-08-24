using Newtonsoft.Json;

namespace FeudalMP.Network.Entity.NetworkMessages
{
    public class GenericNetworkMessage : AbstractNetworkMessage
    {
        public GenericNetworkMessage(){}
        public override void Callback(int peerId, AbstractNetworkMessage abstractNetworkMessage)
        {
            throw new System.NotImplementedException();
        }

        public override AbstractNetworkMessage Convert(string rawJson)
        {
            return JsonConvert.DeserializeObject<GenericNetworkMessage>(rawJson);
        }
    }
}