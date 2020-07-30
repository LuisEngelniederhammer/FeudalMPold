extends Object
class_name Logger

var name:String;

func _init(clazz:String=""):
	if(clazz == ""):
		name = "Log";
	else:
		name = clazz;

func _getTimeFormat() -> String:
	var dateTime_dict = OS.get_datetime();
	return "%s.%s.%s - %s:%s:%s" % [dateTime_dict.day, dateTime_dict.month, dateTime_dict.year, dateTime_dict.hour, dateTime_dict.minute, dateTime_dict.second];

func _write(text:String, level:String) -> void:
	print("[%s] %s.%s :: %s" % [_getTimeFormat(), name, level, text]);
	pass

func info(text:String):
	_write(text, "INFO");

func error(text:String):
	_write(text, "ERROR");

func warn(text:String):
	_write(text, "WARN");
