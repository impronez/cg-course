#version 330 core

in float fragZ;
out vec4 FragColor;

void main()
{
    float brightness = clamp(1.0 - abs(fragZ) / 20.0, 0.0, 1.0);
    vec3 baseColor = vec3(0.0, 0.0, 0.0);
    FragColor = vec4(baseColor * brightness, 1.0);
}
