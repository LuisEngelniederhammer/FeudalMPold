using Godot;
using FeudalMP.Util;
using FeudalMP.Network.Entity.NetworkMessages;
using System.Text.Json;

namespace FeudalMP.Common
{
    public class Main : Node
    {
        public override void _Ready()
        {
            OS.SetWindowTitle("FeudalMP - Version 0.3.2");
            Logger LOG = new Logger(this.GetType().Namespace + "." + this.GetType().Name);
            LOG.Info("Starting FeudalMP");

            ServerInitialSync s = new ServerInitialSync("testdata");
            string jsonString;
            jsonString = JsonSerializer.Serialize(s);
            

            if (isServer())
            {
                GetTree().ChangeScene("res://src/network/server/Server.tscn");
            }
            else
            {
                new SceneService(GetTree()).LoadUI("MainMenu/MainMenu.tscn");
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
