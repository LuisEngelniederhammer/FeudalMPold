extends Node

const mainMenuPath:String = "MainMenu/MainMenu.tscn";

func _ready():
	OS.set_window_title(GlobalConstants.FMP_TITLE + " - Version " + GlobalConstants.FMP_VERSION);
	var logger = Logger.new("Foundation");
	logger.info("Starting FeudalMP");
	
	if(isServer()):
		get_tree().change_scene("res://src/network/server/Server.tscn");
	else:
		SceneService.loadUI(mainMenuPath);

func isServer() -> bool:
	for cliParam in OS.get_cmdline_args():
		if(cliParam == "--fmp-server"):
			return true;
	return false

func getScene() -> Node:
	return get_tree().get_root().get_node(GlobalConstants.NODE_PATH_SCENE);
