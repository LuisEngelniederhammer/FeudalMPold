extends KinematicBody

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
onready var character = $dummy_character;
onready var characterAnimationPlayer = $dummy_character/AnimationPlayer;

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _unhandled_input(event):
	if(event.is_action_pressed("ui_cancel")):
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE);
	
	if (event is InputEventMouseMotion):
		rotate_y(deg2rad(-event.relative.x * mouseSensitivity));
		pivot.rotate_x(deg2rad(-event.relative.y * mouseSensitivity));
		pivot.rotation.x = clamp(pivot.rotation.x, deg2rad(-90), deg2rad(50));
		
		head.rotate_x(deg2rad(-event.relative.y * mouseSensitivity));
		head.rotation.x = clamp(head.rotation.x, deg2rad(-50), deg2rad(30));

func _physics_process(delta):
	direction = Vector3();

	if(Input.is_action_pressed("move_forward")):
		direction -= transform.basis.z;
		if(!characterAnimationPlayer.is_playing()):
			characterAnimationPlayer.play("Run");
	if(Input.is_action_pressed("move_backward")):
		direction += transform.basis.z;
	if(Input.is_action_pressed("move_left")):
		direction -= transform.basis.x;
	if(Input.is_action_pressed("move_right")):
		direction += transform.basis.x;
	if(Input.is_action_just_pressed("jump") && is_on_floor()):
		cc.y = jump;
		print("jump")
	if(Input.is_action_pressed("move_sprint") && is_on_floor()):
		speed = 35;
		print("sprinting")
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
	pass
