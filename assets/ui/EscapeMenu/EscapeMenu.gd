extends Control

func _on_MainMenu_pressed():
	SceneService.loadUI("MainMenu/MainMenu.tscn");
	pass # Replace with function body.


func _on_Exit_pressed():
	get_tree().quit();
	pass # Replace with function body.
