using System;
using Godot;

namespace FeudalMP.Network.Entity
{
    public enum NetworkMessageAction
    {
        CLIENT_SEND_AUTHENTICATION,
        CLIENT_POSITON_UPDATE,
        CLIENT_ANIMATION_STATE_UPDATE,
        CLIENT_PEER_CONNECTION_UPDATE,

        SERVER_INITIAL_SYNC,
        SERVER_CONNECTED_CLIENTS_SYNC,
        SERVER_COMPLETED_SYNC,

        CHAT_MESSAGE
    }
}
