extends "res://src/network/entity/NetworkMessage.gd"
const NetworkMessageAction = preload("res://src/network/entity/NetworkMessageAction.gd");

func _init(steamId64:String).(NetworkMessageAction.CLIENT_SEND_AUTHENTICATION):
	setData(steamId64);
	pass;
