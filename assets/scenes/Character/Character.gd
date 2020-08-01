extends KinematicBody
class_name Character

var speed:int = 10;
var acceleration:int = 10;

var mouseSensitivity:float = 0.1;
var gravity:float = 9.8;
var jump:int = 10;
var cc = Vector3();

var direction:Vector3 = Vector3();
var velocity:Vector3 = Vector3();

onready var pivot = $Pivot;
onready var head = $WeaponPoint;
onready var characterAnimationPlayer = $dummy_character/AnimationPlayer;

var escActive = false;
var escMenu;
var moved;

signal state_change;

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _unhandled_input(event):
	if(!is_network_master()):
		return;
	if(event.is_action_pressed("ui_cancel")):
		if(!escActive):
			Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE);
			escMenu = SceneService.attachUI("EscapeMenu/EscapeMenu.tscn");
			print(escMenu)
			escMenu.show();
			escActive = true;
		else:
			Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED);
			escMenu.hide();
			escMenu.queue_free();
			escActive = false;
	
	if (event is InputEventMouseMotion):
		rotate_y(deg2rad(-event.relative.x * mouseSensitivity));
		pivot.rotate_x(deg2rad(-event.relative.y * mouseSensitivity));
		pivot.rotation.x = clamp(pivot.rotation.x, deg2rad(-90), deg2rad(50));
		
		head.rotate_x(deg2rad(-event.relative.y * mouseSensitivity));
		head.rotation.x = clamp(head.rotation.x, deg2rad(-50), deg2rad(30));
		if(get_tree().get_network_peer() != null):
			Server.rpc_unreliable_id(1, "_update_position", get_tree().get_network_unique_id(), get_translation(), get_rotation());

func _physics_process(delta):
	if(is_network_master()):
		direction = Vector3();
		moved = false;

		if(Input.is_action_pressed("move_forward")):
			direction -= transform.basis.z;
			if(!characterAnimationPlayer.is_playing()):
				characterAnimationPlayer.play("Running");
				if(get_tree().get_network_peer() != null):
					Server.rpc_unreliable_id(1, "_update_animation_state", get_tree().get_network_unique_id(), "Running", false);
			moved = true;
		if(Input.is_action_pressed("move_backward")):
			direction += transform.basis.z;
			if(!characterAnimationPlayer.is_playing()):
				characterAnimationPlayer.play_backwards("Running");
				if(get_tree().get_network_peer() != null):
					Server.rpc_unreliable_id(1, "_update_animation_state", get_tree().get_network_unique_id(), "Running", true);
			moved = true;
		if(Input.is_action_pressed("move_left")):
			direction -= transform.basis.x;
			moved = true;
		if(Input.is_action_pressed("move_right")):
			direction += transform.basis.x;
			moved = true;
		if(Input.is_action_just_pressed("jump") && is_on_floor()):
			cc.y = jump;
			print("jump")
			moved = true;
		if(Input.is_action_pressed("move_sprint") && is_on_floor()):
			speed = 35;
			print("sprinting")
			moved = true;
		else:
			speed = 10;
		
		direction.y -= gravity * delta;
		cc.y -= gravity * delta;
		cc = move_and_slide(cc, Vector3.UP, true);
		
		direction = direction.normalized();
		#velocity = direction * speed;
		#velocity.linear_interpolate(velocity, acceleration * delta);
		velocity = velocity.linear_interpolate(direction * speed, acceleration * delta)
		velocity = move_and_slide(velocity, Vector3.UP, true)
		
		if(get_tree().get_network_peer() != null && (moved || (velocity.length() > 0))):
			Server.rpc_unreliable_id(1, "_update_position", get_tree().get_network_unique_id(), get_translation(), get_rotation());
	pass
