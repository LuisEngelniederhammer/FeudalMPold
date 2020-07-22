extends Button

const mapSelection:String = "MapSelection/MapSelection.tscn"

func _on_Button_pressed():
	SceneService.loadUI(mapSelection);
	pass # Replace with function body.
