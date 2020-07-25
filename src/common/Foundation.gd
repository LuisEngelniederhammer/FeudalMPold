extends Node

const mainMenuPath:String = "MainMenu/MainMenu.tscn";

func _ready():
	SceneService.loadUI(mainMenuPath);

func getNetworkController() -> Node:
	return get_tree().get_root().get_node("/root/Entry/NetworkController");

func getScene() -> Node:
	return get_tree().get_root().get_node("/root/Entry/Scene");

func startServer() -> void:
	var serverResource = load("res://src/server/Server.tscn");
	var serverNode = serverResource.instance();
	##serverNode.set_name("Server");
	get_tree().get_root().get_node("/root/Entry").call_deferred("add_child", serverNode);

func connectClient() -> void:
	var clientResource = load("res://src/client/Client.tscn");
	var clientNode = clientResource.instance();
	##serverNode.set_name("Server");
	get_tree().get_root().get_node("/root/Entry").call_deferred("add_child", clientNode);
