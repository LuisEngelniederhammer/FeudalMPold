extends Node

const ServerDisconnectReason = preload("res://src/common/entity/ServerDisconnectReason.gd");

var logger:Logger;
var clientName:String;

func _init():
	logger = Logger.new("Client");
	pass
	
func joinServer(ip:String, port:int, name:String) -> void:
	clientName = name;
	logger.info("Trying to connect to %s:%s" % [ip, port]);
	var peer = NetworkedMultiplayerENet.new();
	var err = peer.create_client(ip, port);
	if(err > 0):
		logger.error("Cannot connect to server %s:%s -> %s" % [ip,port,GlobalConstants.ERROR_CODES[err]]);
	else:
		get_tree().connect("connected_to_server", Client, "_client_join_success")
		get_tree().connect("connection_failed", Client, "_client_join_failure")
		get_tree().connect("server_disconnected", Client, "_client_server_disconnected")
		logger.info("Client peer created")
		get_tree().set_network_peer(peer);
	pass

remote func syncServerInfo(serverInfoSerialized:String) -> void:
	logger.info("received server info");
	var serverInfo:Dictionary;
	serverInfo = JSON.parse(serverInfoSerialized).get_result();
	logger.info("name: %s, ip: %s, port: %s, players: %s/%s, mapName: %s, mapHash: %s, mapFileName: %s" % [serverInfo.name, serverInfo.ip, serverInfo.port, serverInfo.connectedPlayers, serverInfo.maxPayers,serverInfo.mapInfo.name, serverInfo.mapInfo.fileHash, serverInfo.mapInfo.fileName]);
	logger.info("Loading map " + serverInfo.mapInfo.fileName);
	#TODO handle if the client does not have the map
	SceneService.loadScene(serverInfo.mapInfo.fileName);
	logger.info("sending authentication data to the server");
	Server.rpc_id(1, "authentication", get_tree().get_network_unique_id(), clientName);
	#Server.rpc_id(1,"jipPlayer", get_tree().get_network_unique_id(), clientName);
	pass

remote func disconnectByServer(type:int, reason:String) -> void:
	logger.warn("The server disconnected you! reason=%s type=%s" % [reason, ServerDisconnectReason.TYPE_REASONS[type]]);
	var acceptDialog = preload("res://assets/ui/popup/AcceptServerDisconnect.res").instance();
	acceptDialog.set_text("The server disconnected you!\n\nreason=%s \ntype=%s" % [reason, ServerDisconnectReason.TYPE_REASONS[type]]);
	self.add_child(acceptDialog);
	acceptDialog.popup_centered_clamped();

	get_tree().disconnect("connected_to_server", Client, "_client_join_success");
	get_tree().disconnect("connection_failed", Client, "_client_join_failure");
	get_tree().disconnect("server_disconnected", Client, "_client_server_disconnected");
	get_tree().set_network_peer(null);
	pass

remote func _update_client_position(uid:int, position:Vector3, rotation:Vector3) -> void:
	if(uid != get_tree().get_network_unique_id()):
		#logger.info("updating client position");
		Foundation.getNetworkController().get_node(str(uid)).set_translation(position);
		Foundation.getNetworkController().get_node(str(uid)).set_rotation(rotation);
	pass

remote func _update_animation_state(uid:int, animation:String, backwards:bool = false) -> void:
	if(uid == get_tree().get_network_unique_id()):
		return;
	var targetClient = Foundation.getNetworkController().get_node(str(uid));
	if(!targetClient.characterAnimationPlayer.is_playing()):
		if(backwards):
			targetClient.characterAnimationPlayer.play_backwards(animation);
		else:
			targetClient.characterAnimationPlayer.play(animation);
	pass

remote func jipPlayer(uid:int, name:String, translation = null, rotation = null) -> void:
	var player = preload("res://assets/scenes/Character/Character.tscn").instance();
	var nameTag = preload("res://assets/predef/billboard_text.res").instance();
	nameTag.get_node("Viewport/Label").set_text(str(uid) + " " + name);
	nameTag.set_translation(Vector3(0.0,4.0,0.0));
	player.set_name(str(uid));
	player.set_network_master(uid); # Will be explained later
	player.add_child(nameTag);
	
	if(translation != null && rotation !=null):
		player.set_translation(translation);
		player.set_rotation(rotation);
	Foundation.getNetworkController().add_child(player);
	logger.info("spawning new player jip uid=%s" % uid)
	pass

remote func jipConnectedClients(clients:Array) -> void:
	logger.info("Syncing all already connected clients with local peer. size=%s" % clients.size());
	for client in clients:
		jipPlayer(int(client[0]), client[1], client[2], client[3]);
	pass

func _client_join_success():
	logger.info("connected successfully - trying to obtain server info");
	Server.rpc_id(1, "getServerInfo");
	pass

func _client_join_failure():
	logger.warn("join failure");
	pass

func _client_server_disconnected():
	logger.warn("server disconnected");
	pass

remote func playerDisconnected(uid:int):
	logger.info("trying to remove disconnected player node uis=%s" % [uid])
	var disconnectedPlayer = Foundation.getNetworkController().get_node(str(uid));
	Foundation.getNetworkController().remove_child(disconnectedPlayer);
	disconnectedPlayer.queue_free();
	pass
	
