extends Node

func _ready():
	var peer = NetworkedMultiplayerENet.new();
	peer.create_client("127.0.0.1", 5000);
	get_tree().network_peer = peer;
