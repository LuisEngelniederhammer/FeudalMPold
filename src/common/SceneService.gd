extends Node

func loadUI(name:String):
	var uiResource = load("%s/%s" % [GlobalConstants.PATH_UI, name]);
	var uiNodeTree = uiResource.instance();
	var currentSceneChildren = Foundation.getScene().get_children();
	for childNode in currentSceneChildren:
		Foundation.getScene().remove_child(childNode);
		childNode.queue_free();
	
	Foundation.getScene().add_child(uiNodeTree);
	print_debug("Successfully loaded ui %s" % name);
	pass

func attachUI(name:String):
	var uiResource = load("%s/%s" % [GlobalConstants.PATH_UI, name]);
	var uiTree = uiResource.instance();
	Foundation.getScene().add_child(uiTree);
	return uiTree;

func loadScene(name:String):
	var uiResource = load("%s/%s" % [GlobalConstants.PATH_SCENES, name]);
	var uiNodeTree = uiResource.instance();
	var currentSceneChildren = Foundation.getScene().get_children();
	for childNode in currentSceneChildren:
		Foundation.getScene().remove_child(childNode);
		childNode.queue_free();
	
	Foundation.getScene().add_child(uiNodeTree);
	print_debug("Successfully loaded ui %s" % name);
	pass
	pass
