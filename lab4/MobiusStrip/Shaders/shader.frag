#version 330 core
out vec4 outputColor;

in vec3 fragColor;

void main()
{
    float ambientStrength = 0.1;
    outputColor = vec4(fragColor, 1.0);
}