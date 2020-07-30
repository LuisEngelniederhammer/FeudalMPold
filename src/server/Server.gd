extends Node

remote func test():
	if(!get_tree().is_network_server()):
		return;
	print("test");
	pass
