extends Node

#includes
const Logger = preload("res://src/util/Logger.gd");
const NetworkMessageCallback = preload("res://src/network/boundary/NetworkMessageCallback.gd");

#attributes
var LOG:Logger;
var callbackRegister:Dictionary;

func _init(tree:SceneTree):
	LOG = Logger.new("PacketDispatcher");
	LOG.info("Setting up node signal connections");
	tree.get_multiplayer().connect("network_peer_packet", self, "receive_network_peer_packet");
	return;
	
func receive_network_peer_packet(id:int, packet:PoolByteArray) -> void:
	var packetDict:Dictionary;
	packetDict = parse_json(packet.get_string_from_utf8());
	LOG.info("Received packet from %s: %s" % [id, packetDict]);
	if(callbackRegister.has(int(packetDict.action))):
		callbackRegister.get(int(packetDict.action)).execute(id,packetDict);
	else:
		LOG.warn("Received packet without registered callback to action");
	return;

func registerCallback(networkMessage:int, networkMessageCallback:NetworkMessageCallback) -> void:
	if(networkMessageCallback == null):
		push_error("networkMessageCallback cannot be null when registering to PacketDispatcher");
	callbackRegister[networkMessage] = networkMessageCallback;
	return;
