extends Node
# includes
const ServerAuthenticator = preload("res://src/server/ServerAuthenticator.gd");
const ServerProperties = preload("res://src/server/ServerProperties.gd");
const FMP_ServerInfo = preload("res://src/server/entity/FMP_ServerInfo.gd");
const FMP_ServerMapInfo = preload("res://src/server/entity/FMP_ServerMapInfo.gd");

var logger:Logger;

var authenticator:ServerAuthenticator setget ,getAuthenticator;

var serverInfo:FMP_ServerInfo;
var clients:Dictionary = {};

func _init():
	logger = Logger.new("Server");
	pass

func start() -> void:
	logger.info("Starting FeudalMP Server on port %s" % [ProjectSettings.get_setting("feudal_mp/server/port")]);
	OS.set_window_title(GlobalConstants.FMP_TITLE + " - Version " + GlobalConstants.FMP_VERSION + " SERVER");
	var server = NetworkedMultiplayerENet.new();
	server.create_server(ProjectSettings.get_setting(ServerProperties.PORT), ProjectSettings.get_setting(ServerProperties.MAX_PLAYERS));
	# warning-ignore:return_value_discarded
	get_tree().connect("network_peer_connected", Server, "_server_client_connected");
	# warning-ignore:return_value_discarded
	get_tree().connect("network_peer_disconnected", Server, "_server_client_disconnected");
	get_tree().set_network_peer(server);
	
	authenticator = ServerAuthenticator.new();
	self.add_child(authenticator);

	var mapInfo = FMP_ServerMapInfo.new(ProjectSettings.get_setting(ServerProperties.MAP_NAME), ProjectSettings.get_setting(ServerProperties.MAP_FILENAME), 1);
	serverInfo = FMP_ServerInfo.new(ProjectSettings.get_setting(ServerProperties.NAME), ProjectSettings.get_setting(ServerProperties.HOST), ProjectSettings.get_setting(ServerProperties.PORT), ProjectSettings.get_setting(ServerProperties.MAX_PLAYERS), mapInfo);
	pass

remote func getServerInfo() -> void:
	var senderId = get_tree().get_rpc_sender_id();
	logger.info("Sending serverinfo to %s" % senderId);
	var dictServerInfo = inst2dict(serverInfo);
	dictServerInfo.mapInfo = inst2dict(serverInfo.mapInfo);
	Client.rpc_id(senderId, "syncServerInfo", to_json(dictServerInfo));
	pass

remote func authentication(uid:int, name:String) -> void:
	authenticator.authClient(uid, name);
	pass

remote func _update_position(uid:int, position:Vector3, rotation:Vector3) -> void:
	if(!get_tree().is_network_server()):
		return;
# warning-ignore:unsafe_method_access
	Foundation.getNetworkController().get_node(str(uid)).set_translation(position);
# warning-ignore:unsafe_method_access
	Foundation.getNetworkController().get_node(str(uid)).set_rotation(rotation);
	#logger.info("Broadcasting position to peers for player uid=%s" % uid);
	Client.rpc_unreliable("_update_client_position", uid, position, rotation);
	pass

remote func _update_animation_state(uid:int, animation:String, backwards:bool = false) -> void:
	if(!get_tree().is_network_server()):
		return;
	Client.rpc_unreliable("_update_animation_state", uid, animation, backwards);
	pass

remote func jipPlayer(uid:int, name:String) -> void:
	if(!get_tree().is_network_server()):
		return;
	#authenticate player
	#load player data, position & items
	#sync this uid with all Clients
	#sync all clients with the uid
	clients[uid] = name;
	Client.rpc("jipPlayer", uid, name);
	var childNodes = Foundation.getNetworkController().get_children();
	var uids:Array = [];
	for child in childNodes:
		if(child.get_name() != str(uid)):
			uids.push_back([child.get_name(), clients[int(child.get_name())], child.get_translation(), child.get_rotation()]);
	Client.rpc_id(uid, "jipConnectedClients", uids);
	pass

func _server_client_connected(uid:int):
	logger.info("Client %s connected" % [uid]);
	var player = preload("res://assets/scenes/Character/Character.tscn").instance();
	player.set_name(str(uid));
	player.set_network_master(uid); # Will be explained later
	Foundation.getNetworkController().add_child(player);
	_updateServerInfo();
	pass

func _server_client_disconnected(uid:int):
	logger.info("Client %s disconnected" % [uid]);
	var player = Foundation.getNetworkController().get_node(str(uid));
	Foundation.getNetworkController().remove_child(player);
	player.queue_free();
	Client.rpc("playerDisconnected", uid);
	_updateServerInfo();
	clients.erase(uid);
	pass

func _updateServerInfo() -> void:
	var connectedClients = Foundation.getNetworkController().get_child_count();
	serverInfo.conncetedPlayers = connectedClients
	pass
