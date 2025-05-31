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

3. **Basic Rendering & Logging** ✅ Completed v0.3.0 (2025-05-30)
   - Get any rendering output on screen (clear color, test quad, etc.)
   - Set up and verify logging (to file/console)
   - Ensure render loop and error reporting are robust

4. **Font Rendering & Colored Backgrounds**
   - Load bitmap font atlas (ASCII grid)
   - Render characters as textured quads
   - Render colored backgrounds for each cell
   - Implement basic text layout (rows/columns)

   // Asset folders created:
   //   /assets/fonts
   //   /assets/shaders
   //   /assets/audio
   // Place relevant files in these folders for future tasks.

5. **CRT Visual Effects**
   - Implement scanlines and curved screen shaders
   - Add phosphor glow, bloom, and fade effects
   - Optional: color shifting/flicker

6. **Cursor Implementation**
   - Blinking, stylized cursor (block/underscore)
   - Cursor position updates with input/output

7. **Window Resizing Support**
   - Dynamic adjustment of columns/rows
   - Update OpenGL viewport and buffer layout

8. **Input Handling**
   - Keyboard event handling (OpenTK)
   - Simulate command entry and editing

9. **Simulated Terminal Mode**
   - Simulate PowerShell-like output for UI/UX testing
   - Diagnostic/test mode for buffer stats and effects

10. **Audio Feedback (Optional)**
    - Integrate NAudio or similar
    - Add sound effects for typing, enter, output, errors, startup/shutdown

11. **PowerShell Integration**
    - Launch hidden PowerShell 7 process
    - Bi-directional communication (stdin/stdout/stderr)
    - Async output reading and rendering
    - Forward user input to process

12. **Testing & Validation**
    - Ensure terminal matches PowerShell 7 behavior
    - Validate visual and audio effects
    - Test resizing, performance, and error handling

13. **Extensibility & Future Features**
    - Multiple tabs
    - Theme system
    - ANSI color/escape code support
    - Scripting/macros
    - Export buffer (image/text)
    - Retro file browser

---

**Note:**
- Tasks are ordered to maximize early visual feedback and minimize risk.
- The next milestone is to get basic rendering and logging working.
- PowerShell integration is deferred until the visual emulator is stable.
- Audio and advanced features are optional but recommended for immersion.
