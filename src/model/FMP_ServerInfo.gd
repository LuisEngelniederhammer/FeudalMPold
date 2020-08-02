extends Object
class_name FMP_ServerInfo

var name:String;
var ip:String;
var port:int;
var maxPayers:int;
var connectedPlayers:int;
var mapInfo:FMP_ServerMapInfo;

func _init(_name:String, _ip:String, _port:int, _maxPlayers:int, _mapInfo:FMP_ServerMapInfo):
	self.name = _name;
	self.ip = _ip;
	self.port = _port;
	self.maxPayers = _maxPlayers;
	self.mapInfo = _mapInfo;
	self.connectedPlayers = 0;
	pass
