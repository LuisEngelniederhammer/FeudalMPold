extends Node

var logger:Logger;

func _init():
	logger = Logger.new("Server");
	pass

remote func _update_position(uid:int, position:Vector3, rotation:Vector3) -> void:
	if(!get_tree().is_network_server()):
		return;
	Foundation.getNetworkController().get_node(str(uid)).set_translation(position);
	Foundation.getNetworkController().get_node(str(uid)).set_rotation(rotation);
	#logger.info("Broadcasting position to peers for player uid=%s" % uid);
	Client.rpc_unreliable("_update_client_position", uid, position, rotation);
	pass

remote func _update_animation_state(uid:int, animation:String, backwards:bool = false) -> void:
	if(!get_tree().is_network_server()):
		return;
	Client.rpc_unreliable("_update_animation_state", uid, animation, backwards);
	pass

remote func jipPlayer(uid:int) -> void:
	if(!get_tree().is_network_server()):
		return;
	#authenticate player
	#load player data, position & items
	#sync this uid with all Clients
	#sync all clients with the uid
	Client.rpc("jipPlayer", uid);
	var childNodes = Foundation.getNetworkController().get_children();
	var uids:Array;
	for child in childNodes:
		if(child.get_name() != str(uid)):
			uids.push_back([child.get_name(), child.get_translation(), child.get_rotation()]);
	Client.rpc_id(uid, "jipConnectedClients", uids);
	pass

func _server_client_connected(uid:int):
	logger.info("Client %s connected" % [uid]);
	var player = preload("res://assets/scenes/Character/Character.tscn").instance();
	player.set_name(str(uid));
	player.set_network_master(uid); # Will be explained later
	Foundation.getNetworkController().add_child(player);
	pass

func _server_client_disconnected(uid:int):
	logger.info("Client %s disconnected" % [uid]);
	var player = Foundation.getNetworkController().get_node(str(uid));
	Foundation.getNetworkController().remove_child(player);
	player.queue_free();
	Client.rpc("playerDisconnected", uid);
	pass
