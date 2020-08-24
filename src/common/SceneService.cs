using Godot;

namespace FeudalMP.Common
{
    public class SceneService : Node
    {
        public const string PATH_SCENES = "res://assets/scenes";
        public const string PATH_UI = "res://assets/ui";
        public const string NODE_PATH_BASE = "/root/FeudalMP";
        public const string NODE_PATH_SCENE = NODE_PATH_BASE + "/Scene";
        public const string NODE_PATH_UI = NODE_PATH_BASE + "/UI";
        public Node BaseScene { get; set; }
        public Node BaseUI { get; set; }
        public override void _Ready()
        {
            BaseScene = GetTree().Root.GetNode(NODE_PATH_SCENE);
            BaseUI = GetTree().Root.GetNode(NODE_PATH_UI);
        }
        private void ClearBaseUI()
        {
            BaseUI.Free();
            Node n = new Node();
            n.Name = "UI";
            GetTree().Root.GetNode(NODE_PATH_BASE).AddChild(n);
            BaseUI = GetTree().Root.GetNode(NODE_PATH_UI);
        }
        private void ClearBaseScene()
        {
            BaseScene.Free();
            Node n = new Node();
            n.Name = "Scene";
            GetTree().Root.GetNode(NODE_PATH_BASE).AddChild(n);
            BaseScene = GetTree().Root.GetNode(NODE_PATH_SCENE);
        }
        public void LoadUI(string path)
        {
            CallDeferred(nameof(LoadUIDeferred), System.String.Format("{0}/{1}", PATH_UI, path));
        }
        private void LoadUIDeferred(string path)
        {
            ClearBaseUI();
            ClearBaseScene();
            PackedScene uiResource = ResourceLoader.Load(path) as PackedScene;
            Node uiNodeTree = uiResource.Instance();
            BaseUI.AddChild(uiNodeTree);
        }
        public Node AttachUI(string path)
        {
            PackedScene uiResource = GD.Load(System.String.Format("{0}/{1}", PATH_UI, path)) as PackedScene;
            Node uiNodeTree = uiResource.Instance();
            BaseUI.AddChild(uiNodeTree);
            return uiNodeTree;
        }

        public void LoadScene(string path)
        {
            CallDeferred(nameof(LoadSceneDeferred), System.String.Format("{0}/{1}", PATH_SCENES, path));
        }
        public void LoadSceneDeferred(string path)
        {
            ClearBaseUI();
            ClearBaseScene();
            PackedScene sceneResource = ResourceLoader.Load(path) as PackedScene;
            Node sceneNodeTree = sceneResource.Instance();
            BaseScene.AddChild(sceneNodeTree);
            GetTree().CurrentScene = GetNode(NODE_PATH_BASE);
        }
    }
}