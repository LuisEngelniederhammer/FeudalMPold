using FeudalMP.Common;
using FeudalMP.Network.Server;
using Godot;
using System;

public class MainMenu : Control
{
    public override void _Ready()
    {

    }
    public void _on_Play_pressed() {
        new SceneService(GetTree()).LoadUI("MapSelection/MapSelection.tscn");
    }


    public void _on_Start_Server_pressed()
    {
        new GameServer(GetTree(), (int)ProjectSettings.GetSetting("feudal_mp/server/port"));
    }

    public void _on_Connect_pressed()
    {
        new SceneService(GetTree()).LoadUI("DirectConnect/DirectConnect.tscn");
    }

    public void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}
