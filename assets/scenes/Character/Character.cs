using FeudalMP.Common;
using FeudalMP.Network;
using FeudalMP.Network.Entity.NetworkMessages;
using Godot;

public class Character : KinematicBody
{
    private NetworkService networkService;
    private int speed = 10;
    private int acceleration = 10;

    private float mouseSensitivity = 0.1f;
    private float gravity = 9.8f;
    private int jump = 10;
    private Vector3 cc = new Vector3();
    private Vector3 direction = new Vector3();
    private Vector3 velocity = new Vector3();
    private Spatial pivot;
    private Control escMenu;
    private Spatial head;
    private AnimationPlayer characterAnimationPlayer;
    private bool escActive = false;
    private bool moved = false;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        networkService = new NetworkService(GetTree());
        pivot = GetNode("Pivot") as Spatial;
        head = GetNode("WeaponPoint") as Spatial;
        characterAnimationPlayer = GetNode("dummy_character/AnimationPlayer") as AnimationPlayer;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!IsNetworkMaster())
        {
            return;
        }
        if (@event.IsActionPressed("ui_cancle"))
        {
            if (!escActive)
            {
                Input.SetMouseMode(Input.MouseMode.Visible);
                escMenu = new SceneService(GetTree()).AttachUI("EscapeMenu/EscapeMenu.tscn") as Control;
                escMenu.Show();
                escActive = true;
            }
            else
            {
                Input.SetMouseMode(Input.MouseMode.Captured);
                escMenu.Hide();
                escMenu.QueueFree();
                escActive = false;
            }
        }
        if (@event is InputEventMouseMotion)
        {
            RotateY(Mathf.Deg2Rad(-((InputEventMouseMotion)@event).Relative.x * mouseSensitivity));
            pivot.RotateX(Mathf.Deg2Rad(-((InputEventMouseMotion)@event).Relative.y * mouseSensitivity));
            //@TODO whats happening here ??? check RotateX method vs. direct assignment to x Attribute
            pivot.RotateX(Mathf.Clamp(pivot.Rotation.x, Mathf.Deg2Rad(-90), Mathf.Deg2Rad(50)));

            head.RotateX(Mathf.Deg2Rad(-((InputEventMouseMotion)@event).Relative.y * mouseSensitivity));
            head.RotateX(Mathf.Clamp(head.Rotation.x, Mathf.Deg2Rad(-50), Mathf.Deg2Rad(30)));
            if (GetTree().NetworkPeer != null)
            {
                networkService.toServer(new ClientPositionUpdate(Translation, Rotation));
            }
        }
    }
    public override void _PhysicsProcess(float delta)
    {
        if (IsNetworkMaster())
        {
            direction = new Vector3();
            moved = false;

            if (Input.IsActionPressed("move_forward"))
            {
                direction -= -Transform.basis.z;
                if (!characterAnimationPlayer.IsPlaying())
                {
                    characterAnimationPlayer.Play("Running");
                    if (GetTree().NetworkPeer != null)
                    {
                        networkService.toServer(new ClientAnimationStateUpdate("Running", false));
                    }
                    moved = true;
                }
            }
            if (Input.IsActionPressed("move_backwards"))
            {
                direction += -Transform.basis.z;
                if (!characterAnimationPlayer.IsPlaying())
                {
                    characterAnimationPlayer.PlayBackwards("Running");
                    if (GetTree().NetworkPeer != null)
                    {
                        networkService.toServer(new ClientAnimationStateUpdate("Running", true));
                    }
                    moved = true;
                }
            }
            if (Input.IsActionPressed("move_left"))
            {
                direction -= -Transform.basis.x;
                moved = true;
            }
            if (Input.IsActionPressed("move_right"))
            {
                direction += -Transform.basis.x;
                moved = true;
            }
            if (Input.IsActionPressed("jump") && IsOnFloor())
            {
                cc.y = jump;
                moved = true;
            }
            if (Input.IsActionPressed("move_sprint") && IsOnFloor())
            {
                speed = 35;
                moved = true;
            }
            else { speed = 10; }

            direction.y -= gravity * delta;
            cc.y -= gravity * delta;
            cc = MoveAndSlide(cc, Vector3.Up, true);

            direction = direction.Normalized();
            velocity = velocity.LinearInterpolate(direction * speed, acceleration * delta);
            velocity = MoveAndSlide(velocity, Vector3.Up, true);
            if (GetTree().NetworkPeer != null && (moved || (velocity.Length() > 0)))
            {
                networkService.toServer(new ClientPositionUpdate(Translation, Rotation));
            }

        }
    }

}