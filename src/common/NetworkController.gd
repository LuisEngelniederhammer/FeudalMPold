extends Node

var logger:Logger;

func _init():
	logger = Logger.new("NetworkController");
	logger.info("NetworkController initialized");
	pass

func startServer():
	Server.start();


func joinServer(ip:String, port:int, name:String):
	Client.joinServer(ip, port, name);
