using Godot;
using System;

public class SDLC_State : Node2D
{
	private Label Label;
	public override void _Ready()
	{
		Label = GetNode<Label>("./CanvasLayer/CenterContainer/Label");
		Label.Text = String.Format("FeudalMP v{0} - This version is in active development", ProjectSettings.GetSetting("feudal_mp/application/version"));
	}
}
