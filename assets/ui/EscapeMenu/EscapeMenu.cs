using FeudalMP;
using FeudalMP.Common;
using FeudalMP.Network;
using FeudalMP.Network.Entity.NetworkMessages;
using FeudalMP.Network.Server.Entity;
using Godot;
using static Godot.NetworkedMultiplayerPeer;

public class EscapeMenu : Control
{
    private Godot.Button MainMenuButton;
    public override void _Ready()
    {
        MainMenuButton = GetNode<Godot.Button>("HBoxContainer/VBoxContainer/MainMenu");
        if (GetTree().NetworkPeer != null)
        {
            MainMenuButton.Text = "Disconnect";
        }
    }

    public void _on_MainMenu_pressed()
    {
        ObjectBroker.Instance.NetworkService.toServer(new ClientPeerConnectionUpdate(new ClientRepresentation(GetTree().GetNetworkUniqueId(), new Vector3(), new Vector3()), true), TransferModeEnum.Reliable);
        //FeudalMP.ObjectBroker.Instance.SceneService.LoadUI("MainMenu/MainMenu.tscn");
    }
    
    public void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}
