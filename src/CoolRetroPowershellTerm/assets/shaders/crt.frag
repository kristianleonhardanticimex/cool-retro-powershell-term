#version 330 core
in vec2 vUV;
out vec4 outColor;

uniform sampler2D screenTex;

void main() {
    // Push down the text by offsetting vUV.y
    float yOffset = 0.04; // Push down by 4% of the screen
    vec2 boxUV = vUV + vec2(0.0, yOffset);
    // Clamp to [0,1] to avoid sampling outside
    boxUV = clamp(boxUV, 0.0, 1.0);
    vec2 centered = (boxUV - 0.5) * 2.0;
    float r2 = dot(centered, centered);
    float k = 0.045; // moderate curvature
    vec2 curved = centered * (1.0 + k * r2);
    vec2 sampleUV = curved * 0.5 + 0.5;
    // Clamp to [0,1] to avoid sampling outside
    sampleUV = clamp(sampleUV, 0.0, 1.0);
    vec3 color = texture(screenTex, sampleUV).rgb;
    outColor = vec4(color, 1.0);
}
