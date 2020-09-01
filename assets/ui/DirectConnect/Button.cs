using FeudalMP.Common;
using FeudalMP.Network.Client;
using Godot;
using System;

public class Button : Godot.Button
{
    private LineEdit ipField;
    private LineEdit portField;
    private LineEdit nameField;
    public override void _Ready()
    {
        ipField = GetNode("../ip/LineEdit") as LineEdit;
        portField = GetNode("../port/LineEdit") as LineEdit;
        nameField = GetNode("../name/LineEdit") as LineEdit;
    }

    public void _on_Button_pressed()
    {
        PackedScene networkGameClientResource = GD.Load("res://src/network/client/GameClient.tscn") as PackedScene;
        Node networkGameClientNode = networkGameClientResource.Instance();
        GetNode(SceneService.NODE_PATH_BASE).AddChild(networkGameClientNode);
    }

    public void _on_Button2_pressed()
    {
        FeudalMP.ObjectBroker.Instance.SceneService.LoadUI("MainMenu/MainMenu.tscn");
    }
}
