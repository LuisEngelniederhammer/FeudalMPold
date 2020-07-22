shader_type spatial;

uniform sampler2D displacement_map;
uniform sampler2D normal_map;

uniform sampler2D image_texture;


void fragment(){
	ALBEDO = texture(image_texture, UV).rgb;
	NORMAL = texture(normal_map, UV).rgb;
	
}

void vertex(){
	float height = texture(displacement_map, VERTEX.xz).x * 5.0; //divide by the size of the PlaneMesh
	VERTEX.y += height;
}