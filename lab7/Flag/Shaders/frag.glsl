#version 330 core

in vec2 vPosition;
out vec4 fragColor;

void main() {
    float y = vPosition.y;

    if (y > 0.333)
        fragColor = vec4(1.0, 1.0, 1.0, 1.0);
    else if (y > -0.333)
        fragColor = vec4(0.0, 0.0, 1.0, 1.0);
    else
        fragColor = vec4(1.0, 0.0, 0.0, 1.0);
}
