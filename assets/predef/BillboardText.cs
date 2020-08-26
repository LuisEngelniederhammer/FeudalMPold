using Godot;

public class BillboardText : Spatial
{
    public Label BillboardLabel { get; set; }
    public override void _Ready()
    {
        BillboardLabel = GetNode<Label>("Viewport/Label");
    }
}
