extends MeshInstance

onready var material:Material = self.get_surface_material(0);

func _ready():
	#
	pass


func _on_HSlider_value_changed(value):
	print("amount_waves = %s" % value)
	material.set_shader_param("amount_waves",value);
	pass # Replace with function body.


func _on_HSlider2_value_changed(value):
	print("amplitude = %s" % value)
	material.set_shader_param("amplitude",value);
	pass # Replace with function body.
	pass # Replace with function body.


func _on_HSlider3_value_changed(value):
	print("speed = %s" % value)
	material.set_shader_param("speed",value);
	pass # Replace with function body.
