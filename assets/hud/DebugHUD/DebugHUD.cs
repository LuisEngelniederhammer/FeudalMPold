using Godot;
using System;

public class DebugHUD : Node2D
{
    private Label FPSLabel;
    private Label MemoryLabel;
    private Label ObjectCountLabel;

    public override void _Ready()
    {
        FPSLabel = GetNode<Label>("./CanvasLayer/MarginContainer/HBoxContainer/VBoxContainer/FPS");
        MemoryLabel = GetNode<Label>("./CanvasLayer/MarginContainer/HBoxContainer/VBoxContainer/Memory");
        ObjectCountLabel = GetNode<Label>("./CanvasLayer/MarginContainer/HBoxContainer/VBoxContainer/ObjectCount");
    }

    public override void _PhysicsProcess(float delta){
        FPSLabel.Text = "FPS: " + GD.Str(Performance.GetMonitor(Performance.Monitor.TimeFps));
        MemoryLabel.Text = "Memory: " + GD.Str(Mathf.Round(Performance.GetMonitor(Performance.Monitor.MemoryStatic)/1024/1024)) + "MB";
        ObjectCountLabel.Text = "Object Count: " + GD.Str(Performance.GetMonitor(Performance.Monitor.ObjectCount));
    }
    
}
