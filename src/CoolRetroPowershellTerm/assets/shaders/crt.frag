#version 330 core
in vec2 vUV;
out vec4 outColor;

uniform sampler2D screenTex;

void main() {
    // Passthrough: no margin, no curvature, no scanlines
    vec3 color = texture(screenTex, vUV).rgb;
    outColor = vec4(color, 1.0);
}
