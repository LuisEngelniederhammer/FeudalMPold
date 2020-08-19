extends Node

const NetworkMessage = preload("res://src/network/entity/NetworkMessage.gd");

var tree:SceneTree;

func _init(tree:SceneTree):
	self.tree = tree;
	return;

func toServerRaw(packet:PoolByteArray, transferMode:int = NetworkedMultiplayerPeer.TRANSFER_MODE_UNRELIABLE) -> int:
	return tree.get_multiplayer().send_bytes(packet, NetworkedMultiplayerPeer.TARGET_PEER_SERVER, transferMode);

func toServer(message:NetworkMessage, transferMode:int = NetworkedMultiplayerPeer.TRANSFER_MODE_UNRELIABLE) -> int:
	return toServerRaw(message.toString().to_utf8(), transferMode);

func toClientRaw(targetPeerId:int, packet:PoolByteArray, transferMode:int = NetworkedMultiplayerPeer.TRANSFER_MODE_UNRELIABLE) -> int:
		return tree.get_multiplayer().send_bytes(packet, targetPeerId, transferMode);
	
func toClient(targetPeerId:int, message:NetworkMessage, transferMode:int = NetworkedMultiplayerPeer.TRANSFER_MODE_UNRELIABLE) -> int:
	return toClientRaw(targetPeerId, message.toString().to_utf8(), transferMode);
