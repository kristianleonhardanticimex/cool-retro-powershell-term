# Project Task Breakdown: cool-retro-powershell-term

## Task Prioritization (Top = Highest Priority)

1. **Project Setup & Scaffolding** ✅ Completed v0.1.0 (2025-05-30)
   - Initialize C# solution and main project
   - Set up OpenTK and windowing
   - Prepare asset folders (fonts, shaders, audio)

2. **Text Buffer & Data Structures** ✅ Completed v0.2.0 (2025-05-30)
   - Implement CharEntry struct and 2D buffer
   - Buffer logic for writing/updating characters
   - Simulate terminal output for early testing

3. **Font Rendering**
   - Load bitmap font atlas (ASCII grid)
   - Render characters as textured quads
   - Implement basic text layout (rows/columns)

   // Asset folders created:
   //   /assets/fonts
   //   /assets/shaders
   //   /assets/audio
   // Place relevant files in these folders for future tasks.

4. **CRT Visual Effects**
   - Implement scanlines and curved screen shaders
   - Add phosphor glow, bloom, and fade effects
   - Optional: color shifting/flicker

5. **Cursor Implementation**
   - Blinking, stylized cursor (block/underscore)
   - Cursor position updates with input/output

6. **Window Resizing Support**
   - Dynamic adjustment of columns/rows
   - Update OpenGL viewport and buffer layout

7. **Input Handling**
   - Keyboard event handling (OpenTK)
   - Simulate command entry and editing

8. **Simulated Terminal Mode**
   - Simulate PowerShell-like output for UI/UX testing
   - Diagnostic/test mode for buffer stats and effects

9. **Audio Feedback (Optional)**
   - Integrate NAudio or similar
   - Add sound effects for typing, enter, output, errors, startup/shutdown

10. **PowerShell Integration**
    - Launch hidden PowerShell 7 process
    - Bi-directional communication (stdin/stdout/stderr)
    - Async output reading and rendering
    - Forward user input to process

11. **Testing & Validation**
    - Ensure terminal matches PowerShell 7 behavior
    - Validate visual and audio effects
    - Test resizing, performance, and error handling

12. **Extensibility & Future Features**
    - Multiple tabs
    - Theme system
    - ANSI color/escape code support
    - Scripting/macros
    - Export buffer (image/text)
    - Retro file browser

---

**Note:**
- Tasks are ordered to maximize early visual feedback and minimize risk.
- PowerShell integration is deferred until the visual emulator is stable.
- Audio and advanced features are optional but recommended for immersion.
