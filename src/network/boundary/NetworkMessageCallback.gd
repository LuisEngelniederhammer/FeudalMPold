extends Node

#includes
#const GameServer = preload("res://src/network/server/GameServer.gd");

#attributes
var serverInstance;
var tree:SceneTree;

func _init(tree, serverInstance):
	self.serverInstance = serverInstance;
	self.tree = tree;
	return;

func execute(peer:int, packet:Dictionary) -> void:
	return;

func getServerInstance():
	return serverInstance;

func getTree():
	return tree;
