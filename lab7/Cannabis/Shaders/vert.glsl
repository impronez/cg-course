#version 330 core

layout (location = 0) in float component;

uniform mat4 model;
uniform mat4 projection;

const float PI = 3.14159265359;

float CalculateR(float x) {
    return (1.0 + sin(x)) * (1.0 + 0.9 * cos(8.0 * x)) * (1 + 0.1 * cos(24.0 * x)) * (0.5 + 0.05 * cos(140.0 * x));
}

void main() {
    float r = CalculateR(component);

    vec2 pos = vec2(r * cos(component), r * sin(component));
    pos = vec2(pos.y, -pos.x);
    gl_Position = projection * model * vec4(pos, 0.0, 1.0);
}