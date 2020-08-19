extends "res://src/network/entity/NetworkMessage.gd"
const NetworkMessageAction = preload("res://src/network/entity/NetworkMessageAction.gd");

func _init(translation:Vector3, rotation:Vector3).(NetworkMessageAction.CLIENT_POSITON_UPDATE):
	setData({
		"translation": translation,
		"rotation": rotation,
	});
	pass;
