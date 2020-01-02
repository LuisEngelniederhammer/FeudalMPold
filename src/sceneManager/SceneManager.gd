extends Node;
#class_name SceneManager

var rootSceneNode: Node = null;
var currentScene: Node = null;
var loader: ResourceInteractiveLoader = null;
const loaderPath: String = "res://src/rlevels/loading.tscn";
var time_max: int = 10; # msec

func _ready() -> void:
    rootSceneNode = get_node("/root/FeudalMP/CurrentScene")
    currentScene = rootSceneNode;


func setScene(path, loading = true) -> void:
    if(loading):
        call_deferred("setSceneSafe", path);
    else:
        call_deferred("setSceneUnsafe", path);

func setSceneUnsafe(path) -> void:
    clearScene();
    var s = ResourceLoader.load(path);
    currentScene = s.instance();
    getCurrentSceneRoot().add_child(currentScene);
    get_tree().set_current_scene(currentScene);

func setSceneSafe(path) -> void:
    setScene(loaderPath, false);
    loader = ResourceLoader.load_interactive(path)
    if loader == null: # check for errors
        #show_error()
        push_error("Error loading scene");
        return
    set_process(true)
    currentScene.queue_free() # get rid of the old scene

func _process(_time):
    if loader == null:
        # no need to process anymore
        set_process(false)
        return

    var t = OS.get_ticks_msec()
    while OS.get_ticks_msec() < t + time_max: # use "time_max" to control for how long we block this thread

        # poll your loader
        var err = loader.poll()

        if err == ERR_FILE_EOF: # Finished loading.
            var resource = loader.get_resource()
            loader = null
            set_new_scene(resource)
            break
        elif err == OK:
            print("update")
            update_progress()
        else: # error during loading
            push_error("error during loading")
            loader = null
            break

func update_progress():
    var progress = float(loader.get_stage()) / loader.get_stage_count()
    print(progress)
    getCurrentSceneRoot().get_node("loading/ProgressBar").value = int(progress*100);

func set_new_scene(scene_resource):
    clearScene();
    currentScene = scene_resource.instance()
    getCurrentSceneRoot().add_child(currentScene)

func clearScene():
    rootSceneNode.free();
    rootSceneNode = Node.new();
    rootSceneNode.set_name("CurrentScene");
    get_node("/root/FeudalMP").add_child(rootSceneNode);

func getCurrentSceneRoot() -> Node:
    return get_node("/root/FeudalMP/CurrentScene");
