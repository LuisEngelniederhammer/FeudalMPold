using FeudalMP.Common;
using Godot;

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
        if (GetTree().NetworkPeer != null)
        {
            GetTree().NetworkPeer = null;
        }
        ((SceneService)GetNode("/root/SceneService")).LoadUI("MainMenu/MainMenu.tscn");
    }
    public void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}
