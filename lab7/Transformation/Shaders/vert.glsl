#version 330 core

layout(location = 0) in vec2 position;

uniform float phase;
uniform mat4 view;
uniform mat4 projection;
uniform mat4 model;

const float PI = 3.1415926;

out float fragZ;

vec3 mobiusStripFn(vec2 origin)
{
    float u = origin.x * 2.0 - 1.0;
    float v = origin.y * 2.0 * PI;

    float x = cos(v) * (2.0 + (u / 2.0) * cos(v / 2.0));
    float y = sin(v) * (2.0 + (u / 2.0) * cos(v / 2.0));
    float z = (u / 2.0) * sin(v / 2.0);

    return vec3(x, y, z);
}

vec3 kleinBottleFn(vec2 origin)
{
    vec3 result;
	origin.x = origin.x * 2.0 * PI;
	origin.y = origin.y * 2.0 * PI;

    result.x = (2.0 / 15.0) * (3.0 + cos(origin.x)) * cos(origin.x) * (1.0 + sin(origin.x)) + 2.0 * (1.0 - cos(origin.x) / 2.0) * cos(origin.y);
	result.y = -(2.0 / 15.0) * (3.0 + cos(origin.x)) * sin(origin.x) * (1.0 + sin(origin.x)) + 2.0 * (1.0 - cos(origin.x) / 2.0) * sin(origin.y);
	result.z = (2.0 / 15.0) * cos(origin.x / 2.0) * (1.0 + sin(origin.x)) + ((sqrt(2.0) / 2.0) + (1.0 / sqrt(2.0)) * cos(origin.y)) * sin(origin.x / 2.0);

    return result;
}

void main()
{
    vec3 mobius = mobiusStripFn(position);
    vec3 klein = kleinBottleFn(position);
    vec3 morphed = mix(mobius, klein, phase);

    gl_Position = vec4(morphed, 1.0) * model * view * projection;
}
