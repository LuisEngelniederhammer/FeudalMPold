extends Node
const ServerDisconnectReason = preload("res://src/common/entity/ServerDisconnectReason.gd");

var logger:Logger;

func _init():
	logger = Logger.new("ServerAuthenticator");
	logger.info("Server Authenticator initialized")
	pass

remote func authClient(uid:int, name:String) -> void:
	logger.info("trying to authenticated uid=%s with name=%s" % [uid, name]);
	var authenticated = true;
	var reason = "Password and username did not match";
	var type = ServerDisconnectReason.TYPE.AUTHENTICATION;

	if(!authenticated):
		Client.rpc_id(uid, "disconnectByServer", type, reason);
	pass
