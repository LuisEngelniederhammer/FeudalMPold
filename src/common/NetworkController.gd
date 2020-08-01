extends Node

var logger:Logger;

func _init():
	logger = Logger.new("NetworkController");
	logger.info("NetworkController initialized");
	pass

func startServer():
	logger.info("Starting FeudalMP Server on port %s" % [ProjectSettings.get_setting("feudal_mp/server/port")]);
	OS.set_window_title(GlobalConstants.FMP_TITLE + " - Version " + GlobalConstants.FMP_VERSION + " SERVER");
	var server = NetworkedMultiplayerENet.new();
	server.create_server(ProjectSettings.get_setting("feudal_mp/server/port"), ProjectSettings.get_setting("feudal_mp/server/max_players"));
	get_tree().connect("network_peer_connected", Server, "_server_client_connected");
	var error = get_tree().connect("network_peer_disconnected", Server, "_server_client_disconnected");
	logger.error(GlobalConstants.ERROR_CODES[error]);
	get_tree().set_network_peer(server);
	logger.info("Server peer created");



func joinServer(ip:String, port:int):
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
