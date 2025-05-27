#version 330 core
out vec4 outputColor;

uniform vec3 lightColor;
uniform vec3 lightPos;

in vec2 TexCoords;
in vec3 Normal;
in vec3 FragPos;

uniform sampler2D mainTexture;

void main()
{
    vec4 texColor = texture(mainTexture, TexCoords);

    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
    
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    float specularStrength = 0.1;
    vec3 viewDir = normalize(-FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffuse + specular) * texColor.rgb;
    outputColor = vec4(result, texColor.a);
}
