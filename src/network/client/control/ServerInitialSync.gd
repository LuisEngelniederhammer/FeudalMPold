extends "res://src/network/boundary/NetworkMessageCallback.gd"

const Logger = preload("res://src/util/Logger.gd");

func _init(tree, _serverInstance).(tree, _serverInstance):
	return;

func execute(peer:int, packet:Dictionary):
	var LOG:Logger = Logger.new("ServerInitialSync");
	LOG.info("received initiali sync, loading map %s" % packet.data.mapFilePath);
	SceneService.loadScene(packet.data.mapFilePath);
	
	
	var charResource = load("res://assets/scenes/Character/Character.tscn");
	var charNode = charResource.instance();
	charNode.set_name(str(getTree().get_network_unique_id()));
	charNode.set_network_master(getTree().get_network_unique_id());
	getTree().get_root().get_node(GlobalConstants.NODE_PATH_SCENE).add_child(charNode);
	return;
