using Godot;

namespace FeudalMP.Network.Server.Entity
{
    public enum ClientState
    {
        JIP,
        READY
    }
    public class ClientRepresentation
    {
        public int Id { get; set; }
        public Vector3 Translation { get; set; }
        public Vector3 Rotation { get; set; }
        public ClientState ClientState { get; set; }
        public string Name { get; set; }

        public ClientRepresentation(int Id, Vector3 Translation, Vector3 Rotation)
        {
            this.Id = Id;
            this.Translation = Translation;
            this.Rotation = Rotation;
            this.ClientState = ClientState.JIP;
        }
    }
}