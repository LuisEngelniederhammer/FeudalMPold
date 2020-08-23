using FeudalMP.Common;
using Godot;
using System;

public class EscapeMenu : Control
{
    private Button MainMenuButton;
    public override void _Ready()
    {
        MainMenuButton = GetNode("HBoxContainer/VBoxContainer/MainMenu") as Button;
        if (GetTree().NetworkPeer != null)
        {
            MainMenuButton.Text = "Disconnected";
        }
    }

    public void _on_MainMenu_pressed()
    {
        if (GetTree().NetworkPeer != null)
        {
            GetTree().NetworkPeer = null;
        }
        new SceneService(GetTree()).LoadScene("MainMenu/MainMenu.tscn");
    }
    public void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}
