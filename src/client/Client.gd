extends Node

var logger:Logger;

func _init():
	logger = Logger.new("Client");
	pass
	
remote func _update_client_position(uid:int, position:Vector3, rotation:Vector3) -> void:
	if(uid != get_tree().get_network_unique_id()):
		logger.info("updating client position");
		Foundation.getNetworkController().get_node(str(uid)).set_translation(position);
		Foundation.getNetworkController().get_node(str(uid)).set_rotation(rotation);
	pass

remote func jipPlayer(uid:int, translation = null, rotation = null) -> void:
	var player = preload("res://assets/scenes/Character/Character.tscn").instance();
	player.set_name(str(uid));
	player.set_network_master(uid); # Will be explained later
	if(translation != null && rotation !=null):
		player.set_translation(translation);
		player.set_rotation(rotation);
	Foundation.getNetworkController().add_child(player);
	logger.info("spawning new player jip uid=%s" % uid)
	pass

remote func jipConnectedClients(clients:Array) -> void:
	logger.info("Syncing all already connected clients with local peer. size=%s" % clients.size());
	for client in clients:
		jipPlayer(int(client[0]),client[1],client[2]);
	pass

func _client_join_success():
	logger.info("joined successfully");
	logger.info("Loading map");
	SceneService.loadScene("test/WaterShaderTest.scn");
	logger.info("sending authentication data to the server");
	Server.rpc_id(1,"jipPlayer", get_tree().get_network_unique_id());
	pass

func _client_join_failure():
	logger.warn("join failure");
	pass

func _client_server_disconnected():
	logger.warn("server disconnected");
	pass
