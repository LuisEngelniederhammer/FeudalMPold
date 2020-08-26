using Godot;
using FeudalMP.Util;

namespace FeudalMP.Common
{
	public class Main : Node
	{
		public override void _Ready()
		{
			OS.SetWindowTitle("FeudalMP - Version 0.3.2");
			Logger LOG = new Logger(this.GetType().Namespace + "." + this.GetType().Name);
			LOG.Info("Starting FeudalMP");
			ObjectBroker.Instance.NetworkService = new Network.NetworkService(GetTree());

			if (isServer())
			{
				GetTree().ChangeScene("res://src/network/server/Server.tscn");
			}
			else
			{
				if (((int)ProjectSettings.GetSetting("feudal_mp/application/sdlc_state")) == 1)
				{
					PackedScene SDLC_State = GD.Load("res://assets/hud/DebugHUD/SDLC_State.tscn") as PackedScene;
					GetTree().Root.CallDeferred("add_child", SDLC_State.Instance());
				}
				if (((int)ProjectSettings.GetSetting("feudal_mp/application/debug")) == 1)
				{
					PackedScene DebugHUD = GD.Load("res://assets/hud/DebugHUD/DebugHUD.tscn") as PackedScene;
					GetTree().Root.CallDeferred("add_child", DebugHUD.Instance());
				}
				((SceneService)GetNode("/root/SceneService")).LoadUI("MainMenu/MainMenu.tscn");
			}
		}

		private bool isServer()
		{
			foreach (string cli in OS.GetCmdlineArgs())
			{
				if (cli == "--fmp-server")
				{
					return true;
				}
			}
			return false;
		}
	}
}
