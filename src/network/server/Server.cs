using FeudalMP.Network.Server;
using Godot;
using System;

public class Server : Node
{
    public override void _Ready()
    {
        new GameServer(GetTree(), (int)ProjectSettings.GetSetting("feudal_mp/server/port"));
    }
}
