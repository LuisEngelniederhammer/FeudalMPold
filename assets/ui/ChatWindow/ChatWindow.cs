using FeudalMP.Network.Entity.NetworkMessages;
using Godot;
using System;
using static Godot.NetworkedMultiplayerPeer;

public class ChatWindow : Control
{
    private LineEdit ChatInput;
    public override void _Ready()
    {
        ChatInput = GetNode<LineEdit>("LineEdit");
    }
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
        {
            if (!ChatInput.Text.Empty())
            {
                //Send only if there is text, no need to clutter network traffic of other clients
                //Send to server reliable
                //ClientRepresentation can be null, the server will overwrite that correctly to avoid client manipulation
                FeudalMP.ObjectBroker.Instance.NetworkService.toServer(new ChatMessage(ChatInput.Text, null), TransferModeEnum.Reliable);
                ChatInput.Text = "";
            }
        }
    }
}
