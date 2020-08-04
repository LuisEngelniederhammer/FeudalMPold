extends Object

var clients:Dictionary;

func _init():
    clients = {};
    pass

func add(uid:int, state:int):
    clients[uid] = {"state": state};