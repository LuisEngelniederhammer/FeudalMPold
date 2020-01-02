extends Node

var clients: Dictionary = {};
const MAP_PATH = "res://maps/%s/%s.tscn";
var currentMap: String = "devScene";

onready var CLIENTS_NODE = get_node("/root/FeudalMP/Clients");

var SERVER_PORT: int = 3309;
var MAX_PLAYERS: int = 24;
var MASCHINE_IP: String = "127.0.0.1";

func _ready():
    #get_tree().connect("network_peer_connected", self, "_player_connected")
    #var _t = get_tree().connect("network_peer_disconnected", self, "_player_disconnected")
    #
    #get_tree().connect("connection_failed", self, "_connected_fail")
    #get_tree().connect("server_disconnected", self, "_server_disconnected")
    return;

func startServer() -> void:
    Logger.info("Starting server");
    var peer = NetworkedMultiplayerENet.new();
    peer.create_server(SERVER_PORT, MAX_PLAYERS);
    get_tree().set_network_peer(peer);
    clients[1] = {
        name = "server",
        position = Vector3(0,0,0)
        };
    #TODO: not really sure if the server really needs a loaded instance of the scene
    SceneManager.setScene(MAP_PATH % [currentMap,currentMap]);
    return;

func connectToServer(serverIP:String = MASCHINE_IP, serverPort:int = SERVER_PORT) -> void:
    Logger.info("Trying to connect to %s:%d" %[serverIP, serverPort]);
    var _t = get_tree().connect("connected_to_server", self, "_connected_to_server");
    var peer = NetworkedMultiplayerENet.new();
    peer.create_client(serverIP, serverPort);
    get_tree().set_network_peer(peer);
    return;

remote func sendClientAuthentication(id:int, authData) -> void:
    #if is network server
    #TODO: Authenticate client () go further or kick
    #TODO: add player to the clients list
    #TODO: call JIP_syncMap with the current map
    #TODO: instanciate the player on the server
    #TODO: call JIP_syncNewClient
    if (get_tree().is_network_server()):
        if(!Authentication.authConnectingClient(id, authData)):
            Logger.warn("Client %s failed authentication, kicking client" % [str(id)]);
            return;

        Logger.info("Client %s authenticated successfully, start syncing" % [str(id)]);
        #TODO: this should be loaded from a database
        clients[id] = {name = "player_%s" % id, position = Vector3(0,0,0)};

        #syncing the map to the connecting client
        rpc_id(id, "JIP_syncMap", currentMap);

        #Adding a player representation of a client on the server side for monitoring of state within rpc
        var clientPlayerInstance = load("res://src/Player/Player.tscn").instance();
        clientPlayerInstance.name = str(id);
        clientPlayerInstance.set_network_master(id);
        CLIENTS_NODE.add_child(clientPlayerInstance);

        #Syncing the new player to all clients
        rpc("JIP_syncNewClient", id, clients[id]);
    return;
"""
Sync the current server map to a connecting client
Should be called on a single client that has justconnected
"""
remote func JIP_syncMap(mapName:String):
    Logger.info("Received JIP_syncMap: Syncing map to %s" % mapName);
    SceneManager.setScene(MAP_PATH % [mapName,mapName]);
    pass

"""
Sync the current server map to a connecting client
"""
remote func JIP_syncNewClient(id:int, characterData) -> void:
    clients[id] = characterData;
    var clientPlayerInstance = load("res://src/Player/Player.tscn").instance();
    clientPlayerInstance.name = str(id);
    clientPlayerInstance.set_network_master(id);
    CLIENTS_NODE.add_child(clientPlayerInstance);
    return;

#Signal Listeners
func _connected_to_server():
    Logger.info("Successfully connected to server");
    #TODO: potentially unsafe
    clients[get_tree().get_network_unique_id()] = {
        name = "player" + str(get_tree().get_network_unique_id()),
        position = Vector3(0,0,0)
        };
    rpc_id(1,"sendClientAuthentication", get_tree().get_network_unique_id(), clients[get_tree().get_network_unique_id()]);
