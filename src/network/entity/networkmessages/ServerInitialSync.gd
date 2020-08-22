extends "res://src/network/entity/NetworkMessage.gd"
const NetworkMessageAction = preload("res://src/network/entity/NetworkMessageAction.gd");

func _init(mapFilePath:String).(NetworkMessageAction.SERVER_INITIAL_SYNC):
	setData({"mapFilePath": mapFilePath});
	pass;
