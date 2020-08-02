extends Object
class_name FMP_ServerMapInfo

var name:String;
#should be foldername/mapname.scn within scenes
var fileName:String;
#immutable
var fileHash:String setget setFileHash;
var version:int;

func _init(_name:String, _fileName:String, _version:int):
	self.name = _name;
	self.fileName = _fileName;
	self.version = _version;
	
	var file = File.new();
	fileHash = file.get_md5(GlobalConstants.PATH_SCENES + "/" + _fileName);
	pass

#fileHash is immutable once calculated within the constructor
func setFileHash(_value) -> void:
	return
