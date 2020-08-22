extends Node

#includes
const Logger = preload("res://src/util/Logger.gd");
const PaketDispatcher = preload("res://src/network/PaketDispatcher.gd");
const NetworkMessageAction = preload("res://src/network/entity/NetworkMessageAction.gd");
const ClientSendAuthenticationCallback = preload("res://src/network/server/control/ClientSendAuthenticationCallback.gd");

#attributes
var _eNetInstance:NetworkedMultiplayerENet setget setENetInstance, getENetInstance;
var LOG:Logger;
var dispatcher:PaketDispatcher;
var peers:Dictionary;

func _init(tree:SceneTree, port:int):
	LOG = Logger.new("GameServer");
	dispatcher = PaketDispatcher.new(tree);
	
	_eNetInstance = NetworkedMultiplayerENet.new();
	_eNetInstance.server_relay = false;
	_eNetInstance.create_server(port);
	tree.set_network_peer(_eNetInstance);
	LOG.info("server started on port %s" % [port]);

	_eNetInstance.connect("peer_connected", self, "receive_peer_connected");
	_eNetInstance.connect("peer_disconnected", self, "receive_peer_disconnected");

	dispatcher.registerCallback(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION, ClientSendAuthenticationCallback.new(tree, self));
	return;

#Do not allow to overwrite the eNet instance
func setENetInstance(_value:NetworkedMultiplayerENet) -> void:
	return;
	
func getENetInstance() -> NetworkedMultiplayerENet:
	return _eNetInstance;

func receive_peer_connected(id:int) -> void:
	LOG.info("New client connected id=%s" % id);
	return;

func receive_peer_disconnected(id:int) -> void:
	LOG.info("Client disconnected id=%s" % id);
	return;
