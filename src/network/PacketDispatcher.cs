using System;
using System.Collections.Generic;
using System.Text.Json;
using FeudalMP.Network.Entity;
using FeudalMP.Util;
using Godot;

namespace FeudalMP.Network
{

    public class PacketDispatcher : Godot.Object
    {
        private Dictionary<NetworkMessageAction, NetworkMessage> CallbackRegister;
        private Logger LOG;

        public PacketDispatcher(SceneTree Tree)
        {
            LOG = new Logger(this.GetType().Name);
            LOG.Info("Setting up node signal connections");
            CallbackRegister = new Dictionary<NetworkMessageAction, NetworkMessage>();
            Tree.Multiplayer.Connect("network_peer_packet", this, "receive_network_peer_packet");
        }

        public void receive_network_peer_packet(int id, byte[] packet)
        {
            string rawJson = System.Text.Encoding.UTF8.GetString(packet);
            NetworkMessage abstractNetworkMessage = JsonSerializer.Deserialize<NetworkMessage>(rawJson);
            LOG.Info(String.Format("Received packet from {0}: {1}", id, rawJson));
            if (CallbackRegister.ContainsKey(abstractNetworkMessage.Action))
            {
                CallbackRegister[abstractNetworkMessage.Action].Callback(id, abstractNetworkMessage);
            }
            else
            {
                LOG.Warn("Received packet without registered callback to action");

            }
        }

        public void RegisterCallback(NetworkMessageAction action, NetworkMessage callback)
        {
            CallbackRegister.Add(action, callback);
        }

    }
}