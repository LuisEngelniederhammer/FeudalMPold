using FeudalMP.Network.Server;
using Godot;

public class MainMenu : Control
{
    
    public override void _Ready()
    {
        FeudalMP.ObjectBroker.Instance.SceneService.AttachUI("ChatWindow/ChatWindow.tscn");

        //TODO Check how to ~dtor the GameClient smarter
        //Check if a GameClient is active and remove it
        if (GetTree().Root.HasNode("FeudalMP/GameClient"))
        {
            Node gameClient = GetTree().Root.GetNode("FeudalMP/GameClient");
            GetTree().Root.GetNode("FeudalMP").RemoveChild(gameClient);
            //TODO not sure if it's good to hard deallocated this
            gameClient.Free();
        }
    }
    public void _on_Play_pressed()
    {
        FeudalMP.ObjectBroker.Instance.SceneService.LoadUI("MapSelection/MapSelection.tscn");
    }


    public void _on_Start_Server_pressed()
    {
        PackedScene gameServerPackedScene = GD.Load("res://src/network/server/GameServer.tscn") as PackedScene;
        GameServer gameServer = (GameServer)gameServerPackedScene.Instance();
        AddChild(gameServer);
        //new GameServer(GetTree(), (int)ProjectSettings.GetSetting("feudal_mp/server/port"));
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
