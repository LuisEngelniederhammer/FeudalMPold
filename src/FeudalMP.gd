extends Node

func _ready() -> void:
    if(OS.get_cmdline_args().size() > 0):
        Network.startServer();
    else:
        SceneManager.setScene("res://src/rlevels/mainMenu/mainMenu.tscn");
