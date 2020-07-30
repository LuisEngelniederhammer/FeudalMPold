extends Button

func _on_Connect_pressed():
	Foundation.getNetworkController().joinServer("127.0.0.1", 9913);
	pass # Replace with function body.
