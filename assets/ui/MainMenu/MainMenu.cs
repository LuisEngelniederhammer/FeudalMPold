using FeudalMP.Network.Server;
using Godot;

public class MainMenu : Control
{
    public override void _Ready()
    {
        FeudalMP.ObjectBroker.Instance.SceneService.AttachUI("ChatWindow/ChatWindow.tscn");
    }
    public void _on_Play_pressed()
    {
        FeudalMP.ObjectBroker.Instance.SceneService.LoadUI("MapSelection/MapSelection.tscn");
    }


    public void _on_Start_Server_pressed()
    {
        new GameServer(GetTree(), (int)ProjectSettings.GetSetting("feudal_mp/server/port"));
    }

    public void _on_Connect_pressed()
    {
        FeudalMP.ObjectBroker.Instance.SceneService.LoadUI("DirectConnect/DirectConnect.tscn");
    }

    public void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}
