#version 330 core
out vec4 FragColor;

in vec3 FragPos;
in vec3 Normal;
in vec3 Color;
in vec2 TexCoord;

uniform vec3 lightPos;
uniform float ambientStrength;
uniform vec3 ambientColor;
uniform vec3 diffuseColor;

void main()
{
    vec3 ambient = ambientStrength * ambientColor;
    
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * diffuseColor;
    
    vec3 result = (ambient + diffuse) * Color;
    FragColor = vec4(result, 1.0);
}