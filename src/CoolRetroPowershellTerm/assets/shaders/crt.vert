#version 330 core
layout(location = 0) in vec2 inPos;
layout(location = 1) in vec2 inUV;
out vec2 vUV;
void main() {
    gl_Position = vec4(inPos, 0, 1);
    vUV = inUV;
}
