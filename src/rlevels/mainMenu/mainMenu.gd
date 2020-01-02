extends Node
const SERVER_PORT: int = 3390;

func _ready() -> void:
    var _t =get_node("menu/startButton").connect("pressed", self, "_on_startButton_pressed");
    _t = get_node("menu/exitButton").connect("pressed", self, "_on_exitButton_pressed");


func _on_startButton_pressed():
    Network.connectToServer(Network.MASCHINE_IP, Network.SERVER_PORT);

func _on_exitButton_pressed() -> void:
    get_tree().quit();
    pass # Replace with function body.
