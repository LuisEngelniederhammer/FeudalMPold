extends Node

export var port:int = 5000;
export var maxPlayers:int = 200;

func _ready():
	var server = NetworkedMultiplayerENet.new();
	server.create_server(port, maxPlayers);
	get_tree().set_network_peer(server);
	
	get_tree().connect("network_peer_connected",    self, "_client_connected"   )
	get_tree().connect("network_peer_disconnected", self, "_client_disconnected")
	pass

func _client_connected(id):
	print('Client ' + str(id) + ' connected to Server');
	var newClient = load("res://src/server/ClientRepresentation.tscn").instance();
	newClient.set_name(str(id));
	get_tree().get_root().get_node("Entry/Server/Clients").add_child(newClient);
	pass

func _client_disconnected(id):
	get_tree().get_root().get_node("Entry/Server/Clients/" + str(id)).queue_free();
	pass
