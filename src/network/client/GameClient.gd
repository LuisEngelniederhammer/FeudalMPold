extends Node

#includes
const Logger = preload("res://src/util/Logger.gd");
const NetworkService = preload("res://src/network/NetworkService.gd");
const ClientSendAuthentication = preload("res://src/network/entity/networkmessages/ClientSendAuthentication.gd");

#attributes
var _eNetInstance:NetworkedMultiplayerENet;
var LOG:Logger;
var networkService:NetworkService;

func joinServer(tree:SceneTree, ip:String, port:int, name:String) -> void:
	LOG = Logger.new("GameClient");
	_eNetInstance = NetworkedMultiplayerENet.new();
	_eNetInstance.server_relay = false;
	_eNetInstance.create_client(ip, port);
	tree.set_network_peer(_eNetInstance);

	networkService = NetworkService.new(tree);

	_eNetInstance.connect("connection_failed", self, "receive_connection_failed");
	_eNetInstance.connect("connection_succeeded", self, "receive_connection_succeeded");

	return

func receive_connection_failed() -> void:
	LOG.warn("Cannot connect to server");
	return;

func receive_connection_succeeded() -> void:
	LOG.warn("Successfully connected to server");
	networkService.toServer(ClientSendAuthentication.new("mySteamId"), 2);
	return;
