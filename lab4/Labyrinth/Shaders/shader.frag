#version 330 core
out vec4 outputColor;

uniform vec3 lightColor;
uniform vec3 lightPos;

in vec3 outColor;
in vec3 Normal;
in vec3 FragPos;

void main()
{
    float ambientStrength = 0.2;
    vec3 ambient = ambientStrength * lightColor;
    
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    float specularStrength = 0.2;
    vec3 viewDir = normalize(-FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffuse + specular) * outColor;
    outputColor = vec4(result, 1.0);
}