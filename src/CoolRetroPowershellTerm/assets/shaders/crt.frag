#version 330 core
in vec2 vUV;
out vec4 outColor;

uniform sampler2D screenTex;

void main() {
    // Reduce left/right margin, keep vertical offset
    float xMargin = 0.02; // 2% margin left/right
    float yOffset = 0.04; // Push down by 4% of the screen
    vec2 boxUV = vec2(mix(xMargin, 1.0 - xMargin, vUV.x), vUV.y + yOffset);
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
    // Apply retro orange tint (FFAB2B)
    vec3 retroColor = vec3(1.0, 0.67, 0.17); // #FFAB2B normalized
    color *= retroColor;
    // --- Glow effect (true fullscreen, not masked by text area or curvature) ---
    float glowStrength = 0.32;
    float glowSize = 0.012;
    vec3 glow = vec3(0.0);
    float totalWeight = 0.0;
    for (int x = -3; x <= 3; ++x) {
        for (int y = -3; y <= 3; ++y) {
            float weight = 1.0 - 0.13 * (abs(x) + abs(y));
            vec2 offset = vec2(float(x), float(y)) * glowSize;
            // Use vUV for the glow kernel center, and sample the original framebuffer at vUV+offset
            vec2 glowUV = clamp(vUV + offset, 0.0, 1.0);
            vec3 glowSample = texture(screenTex, glowUV).rgb * retroColor;
            // Remove threshold: accumulate glow everywhere for a true fullscreen effect
            glow += mix(vec3(0.0), glowSample, 0.6) * weight;
            totalWeight += weight;
        }
    }
    if (totalWeight > 0.0) glow /= totalWeight;
    glow *= glowStrength;
    // Combine glow and main color
    outColor = vec4(min(color + glow, 1.0), 1.0);
}
