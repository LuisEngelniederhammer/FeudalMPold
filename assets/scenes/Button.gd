extends Button

const mapSelection:String = "MapSelection.tscn"

func _ready():
	pass

enum TEST {
	A,
	B
}

func _on_Button_pressed():
	SceneService.loadUIScene(mapSelection);
	print(GlobalConstants.ERROR_CODES[1]);
	pass # Replace with function body.
