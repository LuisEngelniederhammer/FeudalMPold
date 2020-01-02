extends Spatial
func _ready() -> void:
    var _ignore = get_node("interaction_area").connect("body_entered", self, "_on_interaction_area_body_entered")
    pass # Replace with function body.



func _on_interaction_area_body_entered(_body: Node) -> void:
    print_debug("entered area")
    pass # Replace with function body.
