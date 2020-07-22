extends Node

func loadUI(name:String):
	var status = get_tree().change_scene("%s/%s" % [GlobalConstants.PATH_UI, name]);
	if(status != OK):
		print_debug("Unable to load ui, error: %s" % GlobalConstants.ERROR_CODES[status]);
	pass

func attachUI(name:String):
	var uiResource = load("%s/%s" % [GlobalConstants.PATH_UI, name]);
	var uiTree = uiResource.instance();
	get_tree().get_current_scene().call_deferred("add_child", uiTree);
	return uiTree;

func loadScene(name:String):
	var status = get_tree().change_scene("%s/%s" % [GlobalConstants.PATH_SCENES, name]);
	if(status != OK):
		print_debug("Unable to load scene, error: %s" % GlobalConstants.ERROR_CODES[status]);
	pass
