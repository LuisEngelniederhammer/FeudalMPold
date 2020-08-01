extends Control

onready var optionButton:OptionButton = $VBoxContainer/OptionButton;
onready var scenes:Array;

func _ready():
	refreshOptionButton();
	pass

func refreshOptionButton():
	scenes.clear();
	optionButton.clear();
	getSceneFiles();
	for sceneFile in scenes:
		optionButton.add_item(sceneFile);

func getSceneFiles():
	var dir = Directory.new();
	dir.open(GlobalConstants.PATH_SCENES);
	#skip . and ..
	dir.list_dir_begin(true, true);

	while true:
		var currentDirElement = dir.get_next();
		var path = dir.get_current_dir() + "/" + currentDirElement;
		if (currentDirElement == ""):
			break;
		elif (dir.current_is_dir() && getLocalSceneFiles(path) != ""):
			var localScene = currentDirElement + "/" + getLocalSceneFiles(path);
			scenes.append(localScene);
			print("found %s" % localScene);
		
	dir.list_dir_end();
	pass;

#This will load all "*.scn" files from all subdirectories of res://assets/scenes
func getLocalSceneFiles(localPath:String):
	
	var scnFile:String;
	var dir = Directory.new();
	dir.open(localPath);
	dir.list_dir_begin(true,true);

	while true:
		var file = dir.get_next()
		if file == "":
			break
		elif file.ends_with(".scn"):
			scnFile = file;
			break;
	
	dir.list_dir_end()
	return scnFile;


func _on_Button_pressed():
	refreshOptionButton();
	pass # Replace with function body.


func _on_Start_pressed():
	print("open scene %d" % optionButton.get_selected_id());
	var player = preload("res://assets/scenes/Character/Character.tscn").instance();
	player.set_name(str(get_tree().get_network_unique_id()));
	player.set_network_master(get_tree().get_network_unique_id()); # Will be explained later
	Foundation.getNetworkController().add_child(player);
	SceneService.loadScene(scenes[optionButton.get_selected_id()]);
	pass # Replace with function body.
