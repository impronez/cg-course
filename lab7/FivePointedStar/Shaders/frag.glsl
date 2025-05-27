#version 330 core

in vec2 vPosition;
out vec4 fragColor;

const float PI = 3.1415926;

bool PointIsOnTheLeft(vec2 p0, vec2 p1, vec2 p)
{
    vec2 p0p1 = p1 - p0;
    vec2 n = vec2(-p0p1.y, p0p1.x);
    return dot(p - p0, n) > -1e-6;
}

bool PointIsInsideTriangle(vec2 p0, vec2 p1, vec2 p2, vec2 p)
{
    return
        PointIsOnTheLeft(p0, p1, p) &&
        PointIsOnTheLeft(p1, p2, p) &&
        PointIsOnTheLeft(p2, p0, p);
}

void main()
{
    vec2 p = vPosition;

    const vec2 center = vec2(0.0, 0.0);
    const float outerRadius = 0.5;
    const float innerRadius = 0.2;

    vec2 star[10];
    for (int i = 0; i < 10; i++) {
        float angle = PI / 2.0 + PI / 5.0 * i;
        float r = (i % 2 == 0) ? outerRadius : innerRadius;
        star[i] = vec2(cos(angle), sin(angle)) * r;
    }

    bool inside = false;
    for (int i = 0; i < 10; i++) {
        vec2 p0 = center;
        vec2 p1 = star[i];
        vec2 p2 = star[(i + 1) % 10];
        if (PointIsInsideTriangle(p0, p1, p2, p)) {
            inside = true;
            break;
        }
    }

    fragColor = inside ? vec4(1.0, 0.0, 0.0, 1.0) : vec4(1.0, 1.0, 1.0, 1.0);
}
