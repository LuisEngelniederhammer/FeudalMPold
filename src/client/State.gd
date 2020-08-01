extends Node
class_name State

func _ready():
	connect("state_changed", self, "_state_change");
	pass

func _state_change():
	pass
