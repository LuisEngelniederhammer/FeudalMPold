extends Button



func _on_Create_Server_pressed():
# warning-ignore:unsafe_method_access
	Foundation.getNetworkController().startServer();
	pass # Replace with function body.
