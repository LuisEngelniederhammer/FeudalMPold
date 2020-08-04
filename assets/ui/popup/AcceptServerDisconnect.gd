extends Popup

func _ready():
	set_exclusive(true);

func _on_AcceptDialog_confirmed():
	SceneService.loadUI("MainMenu/MainMenu.tscn");
	self.queue_free();
	pass # Replace with function body.
