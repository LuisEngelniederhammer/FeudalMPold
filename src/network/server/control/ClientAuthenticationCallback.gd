extends "res://src/network/boundary/NetworkMessageCallback.gd"

const Logger = preload("res://src/util/Logger.gd");

func execute(peer:int, packet:Dictionary):
	var LOG:Logger = Logger.new("ClientAuthenticationCallback");
	LOG.info("yes callback peer=%s data=%s" % [peer, to_json(packet)]);
	return;
