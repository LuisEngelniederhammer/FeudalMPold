using Godot;
using System;

namespace FeudalMP.Common
{
    public class SceneService : Node
    {
        public const string PATH_SCENES = "res://assets/scenes";
        public const string PATH_UI = "res://assets/ui";
        public const string NODE_PATH_BASE = "/root/FeudalMP";
        public const string NODE_PATH_SCENE = NODE_PATH_BASE + "/Scene";
        private SceneTree Tree;
        public SceneService(SceneTree Tree)
        {
            this.Tree = Tree;
        }

        public Node GetScene()
        {
            return Tree.Root.GetNode(NODE_PATH_SCENE);
        }

        public void LoadUI(string path)
        {
            PackedScene uiResource = ResourceLoader.Load(String.Format("{0}/{1}", PATH_UI, path)) as PackedScene;
            Node uiNodeTree = uiResource.Instance();
            foreach (Node child in GetScene().GetChildren())
            {
                GetScene().RemoveChild(child);
                child.QueueFree();
            }
            GetScene().AddChild(uiNodeTree);
        }

        public Node AttachUI(string path)
        {
            PackedScene uiResource = ResourceLoader.Load(String.Format("{0}/{1}", PATH_UI, path)) as PackedScene;
            Node uiNodeTree = uiResource.Instance();
            GetScene().AddChild(uiNodeTree);
            return uiNodeTree;
        }

        public void loadScene(string path)
        {
            PackedScene sceneResource = ResourceLoader.Load(String.Format("{0}/{1}", PATH_SCENES, path)) as PackedScene;
            Node sceneNodeTree = sceneResource.Instance();
            foreach (Node child in GetScene().GetChildren())
            {
                GetScene().RemoveChild(child);
                child.QueueFree();
            }
            GetScene().AddChild(sceneNodeTree);
        }
    }
}