#version 330 core

layout(location = 0) in vec3 aPosition;  
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec3 aNormal;

out vec3 outColor;
out vec3 Normal;
out vec3 FragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    gl_Position = vec4(aPosition, 1.0) * model * view * projection; 
	outColor = aColor;
	FragPos = vec3(vec4(aPosition, 1.0) * model);
    Normal = aNormal * mat3(model);
}