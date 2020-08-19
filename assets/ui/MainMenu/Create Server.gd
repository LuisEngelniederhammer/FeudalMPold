extends Button

const GameServer = preload("res://src/network/server/GameServer.gd");

func _on_Create_Server_pressed():
	GameServer.new(get_tree(), ProjectSettings.get_setting("feudal_mp/server/port"));
	pass # Replace with function body.
