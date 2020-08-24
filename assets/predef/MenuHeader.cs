using Godot;
using System;

public class MenuHeader : HBoxContainer
{
    private Label titleText;
    public override void _Ready()
    {
        titleText = GetNode("CenterContainer/Label") as Label;
        titleText.Text = String.Format("FeudalMP v{0}", ProjectSettings.GetSetting("feudal_mp/application/version"));
    }

}
