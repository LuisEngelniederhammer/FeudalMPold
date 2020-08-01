extends Button

onready var ipField:LineEdit = get_node("../ip/LineEdit");
onready var portField:LineEdit = get_node("../port/LineEdit");


func _on_Button_pressed():
	Foundation.getNetworkController().joinServer(ipField.get_text(),int(portField.get_text()));
	pass # Replace with function body.


func _on_Button2_pressed():
	SceneService.loadUI("MainMenu/MainMenu.tscn");
	pass # Replace with function body.
