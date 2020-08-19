extends "res://src/network/entity/NetworkMessage.gd"
const NetworkMessageAction = preload("res://src/network/entity/NetworkMessageAction.gd");

func _init(animationName:String, backwards:bool = false).(NetworkMessageAction.CLIENT_ANIMATION_STATE_UPDATE):
	setData({
		"animationName": animationName,
		"backwards": backwards,
	});
	pass;
