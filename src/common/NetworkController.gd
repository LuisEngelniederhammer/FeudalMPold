extends Node

var logger:Logger;

func _init():
	logger = Logger.new("NetworkController");
	logger.info("NetworkController initialized");
	pass

func startServer():
	logger.info("Starting FeudalMP Server");
	OS.set_window_title(GlobalConstants.FMP_TITLE + " - Version " + GlobalConstants.FMP_VERSION + " SERVER");
	var server = NetworkedMultiplayerENet.new();
	server.create_server(ProjectSettings.get_setting("feudal_mp/server/port"), ProjectSettings.get_setting("feudal_mp/server/max_players"));
	get_tree().connect("network_peer_connected", self, "_server_client_connected");
	get_tree().connect("network_peer_disconnected", self, "_server_client_disconnected");
	get_tree().set_network_peer(server);
	logger.info("Server peer created");

func _server_client_connected(uid:int):
	logger.info("Client %s connected" % [uid]);
	pass

func _server_client_disconnected(uid:int):
	logger.info("Client %s disconnected" % [uid]);
	pass

func joinServer(ip:String, port:int):
	logger.info("Trying to connect to %s:%s" % [ip, port]);
	var peer = NetworkedMultiplayerENet.new();
	var err = peer.create_client(ip, port);
	if(err > 0):
		logger.error("Cannot connect to server %s:%s -> %s" % [ip,port,GlobalConstants.ERROR_CODES[err]]);
	else:
		get_tree().connect("connected_to_server", self, "_client_join_success")
		get_tree().connect("connection_failed", self, "_client_join_failure")
		get_tree().connect("server_disconnected", self, "_client_server_disconnected")
		logger.info("Client peer created")
		get_tree().set_network_peer(peer);
	pass

func _client_join_success():
	logger.info("joined successfully");
	logger.info("sending authentication data to the server");
	rpc_id(1, "authClient", get_tree().get_network_unique_id());
	Server.rpc_id(1,"test");
	pass

func _client_join_failure():
	logger.warn("join failure");
	pass

func _client_server_disconnected():
	logger.warn("server disconnected");
	pass

remote func authClient(uid:int):
	logger.info("authenticating client %s" % [uid]);
	Server.rpc_id(uid,"test");
	pass
