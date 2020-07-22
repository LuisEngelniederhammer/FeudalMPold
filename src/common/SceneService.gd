extends Node

func loadUIScene(name:String):
	var status = get_tree().change_scene("%s/%s" % [GlobalConstants.PATH_UI, name]);
	if(status != OK):
		printerr("Unable to load scene, error: %s" % status);
	pass
