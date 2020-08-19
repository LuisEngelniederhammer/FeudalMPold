extends Object

var action:int;
var data setget setData, getData;

func _init(action:int):
	self.action = action;
	return;

func setData(value) -> void:
	data = value;
	return;

func getData():
	return data;

func toString() -> String:
	return to_json({"action": action, "data": data});
