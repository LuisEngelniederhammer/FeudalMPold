extends Node

#includes
const GameServer = preload("res://src/network/server/GameServer.gd")

func _ready():
	var server:GameServer;
	server = GameServer.new(get_tree(), ProjectSettings.get_setting("feudal_mp/server/port"));
