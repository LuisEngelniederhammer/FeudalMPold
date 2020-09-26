using System;
using System.Collections.Generic;
using FeudalMP.Network.Entity;
using FeudalMP.Network.Entity.NetworkMessages;
using FeudalMP.Util;
using Godot;
using Newtonsoft.Json;

namespace FeudalMP.Network
{
    public class PacketDispatcher : Godot.Node
    {
        private Dictionary<NetworkMessageAction, AbstractNetworkMessage> CallbackRegister;
        private Logger LOG;

        public override void _Ready()
        {
            LOG = new Logger(this.GetType().Name + System.Guid.NewGuid().ToString());
            LOG.Info("Setting up node signal connections");
            CallbackRegister = new Dictionary<NetworkMessageAction, AbstractNetworkMessage>();
            GetTree().Multiplayer.Connect("network_peer_packet", this, "receive_network_peer_packet");
        }

        public void receive_network_peer_packet(int id, byte[] packet)
        {
            string rawJson = System.Text.Encoding.UTF8.GetString(packet);
            LOG.Info(String.Format("Received packet from {0}: {1}", id, rawJson));
            GenericNetworkMessage genericNetworkMessage = JsonConvert.DeserializeObject<GenericNetworkMessage>(rawJson);
            LOG.Info(String.Format("{0} by {1}", genericNetworkMessage.Action, GD.Str(id)));
            if (CallbackRegister.ContainsKey(genericNetworkMessage.Action))
            {
                try
                {
                    CallbackRegister[genericNetworkMessage.Action].Callback(id, CallbackRegister[genericNetworkMessage.Action].Convert(rawJson));
                }
                catch (Exception e)
                {
                    LOG.Warn(String.Format("Callback on {0} resulted in error:\n{1}", genericNetworkMessage.Action, e.Message));
                }
            }
            else
            {
                LOG.Warn("Received packet without registered callback to action");

            }
        }

        public void RegisterCallback(NetworkMessageAction action, AbstractNetworkMessage callback)
        {
            CallbackRegister.Add(action, callback);
        }

        public override void _ExitTree()
        {
            //dispatcher.QueueFree();
            GD.Print("PacketDispatcher@_ExitTree ");
        }

    }
}
