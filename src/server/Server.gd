extends Node

remote func test():
	if(!get_tree().is_network_server()):
		return;
	print("server only rpc function on different node");
	pass
