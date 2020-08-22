extends "res://src/network/boundary/NetworkMessageCallback.gd"

const NetworkService = preload("res://src/network/NetworkService.gd");
const Logger = preload("res://src/util/Logger.gd");

const ServerInitialSync = preload("res://src/network/entity/networkmessages/ServerInitialSync.gd");

var network:NetworkService;

func _init(tree, _serverInstance).(tree, _serverInstance):
	network = NetworkService.new(tree);
	return;

func execute(peer:int, packet:Dictionary):
	var LOG:Logger = Logger.new("ClientSendAuthenticationCallback");
	self.getServerInstance().peers[peer] = packet.data;
	
	LOG.info("Added %s to server peer list with name %s" % [peer, packet.data]);
	network.toClient(peer, ServerInitialSync.new("DevWorld/DevWorld.scn"), 2);
	return;
