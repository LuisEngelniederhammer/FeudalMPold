using FeudalMP.Common;
using FeudalMP.Network.Server;
using Godot;

public class MainMenu : Control
{
	public override void _Ready()
	{
	}
	public void _on_Play_pressed()
	{
		((SceneService)GetNode("/root/SceneService")).LoadUI("MapSelection/MapSelection.tscn");
	}


	public void _on_Start_Server_pressed()
	{
		new GameServer(GetTree(), (int)ProjectSettings.GetSetting("feudal_mp/server/port"));
	}

	public void _on_Connect_pressed()
	{
		((SceneService)GetNode("/root/SceneService")).LoadUI("DirectConnect/DirectConnect.tscn");
	}

	public void _on_Exit_pressed()
	{
		GetTree().Quit();
	}
}
