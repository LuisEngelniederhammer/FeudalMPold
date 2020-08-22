extends Button

const GameClient = preload("res://src/network/client/GameClient.gd");

onready var ipField:LineEdit = get_node("../ip/LineEdit");
onready var portField:LineEdit = get_node("../port/LineEdit");
onready var nameField:LineEdit = get_node("../name/LineEdit");


func _on_Button_pressed():
	var gameClient = GameClient.new();
	gameClient.joinServer(get_tree(), ipField.get_text(),int(portField.get_text()), nameField.get_text());
	pass # Replace with function body.


func _on_Button2_pressed():
	SceneService.loadUI("MainMenu/MainMenu.tscn");
	pass # Replace with function body.
