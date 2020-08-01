extends Control

onready var MainMenuButton:Button = $HBoxContainer/VBoxContainer/MainMenu;

func _ready():
	if(get_tree().get_network_peer() != null):
		MainMenuButton.set_text("Disconnect");
	pass

func _on_MainMenu_pressed():
	if(get_tree().get_network_peer() != null):
		get_tree().set_network_peer(null);
	var clientRepresentations = Foundation.getNetworkController().get_children();
	for client in clientRepresentations:
		Foundation.getNetworkController().remove_child(client);
		client.queue_free();
	SceneService.loadUI("MainMenu/MainMenu.tscn");
	pass # Replace with function body.


func _on_Exit_pressed():
	get_tree().quit();
	pass # Replace with function body.
