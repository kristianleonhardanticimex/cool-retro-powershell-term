# CRT Terminal Emulator – Requirements Specification

## Overview
The project is to develop a fully functional terminal emulator that emulates the look and feel of a retro CRT monitor while using a hidden PowerShell 7 instance for executing commands. The emulator must support advanced visual effects, real-time interactivity, and optional audio feedback. The purpose is both aesthetic and functional: a nostalgic, immersive terminal experience that behaves like a real command line.

## 1. Functional Requirements
### 1.1 Terminal Behavior
- Bi-directional communication with a hidden PowerShell 7 process
- User input forwarded character-by-character or line-by-line
- Asynchronous output reading and rendering
- Standard console behavior: command execution, directory navigation, stdout/stderr support

### 1.2 Cursor
- Blinking cursor at current write position (1 Hz, stylized)
- Cursor updates as characters are typed or output is written

### 1.3 Window Resizing
- Dynamic window resizing
- Visible columns/rows adapt to window size and font metrics
- OpenGL viewport and character positions update accordingly

## 2. Visual Requirements
### 2.1 CRT Effects
- Curved screen distortion (vertex shader)
- Scanlines (interlaced display simulation)
- Phosphor glow and bloom on new characters
- Fading characters (solid glow to final form)
- Optional: color shifting and flicker

### 2.2 Font Rendering
- Bitmap fonts (ASCII glyph atlas)
- Each character as a quad textured from font atlas
- (Optional) SDF font support

## 3. Performance & Architecture
### 3.1 Rendering Engine
- OpenGL via OpenTK in C#
- Shaders for distortion, glow, fade
- 60 FPS target
- Efficient text buffer rendering (VAO/VBO, instanced rendering optional)

### 3.2 Text Buffer
- 2D buffer of character cells:
  ```csharp
  struct CharEntry {
      char value;
      float timeWritten;
      bool isNewlyWritten;
  }
  ```
- Dynamic writing/updating as output is received

## 4. Audio Requirements
### 4.1 Event Sounds (Optional)
- Typing (click/typewriter)
- Enter/command (blip)
- Output (scroll/static)
- Error (buzz/glitch)
- Startup (CRT power-on/static)
- Shutdown (fade/pop/hum)

### 4.2 Implementation
- .NET-compatible audio library (e.g., NAudio)
- Audio clips as external .wav/.mp3
- Non-overlapping playback for repeated keypresses

## 5. Technology Stack
- Language: C#
- Graphics: OpenGL via OpenTK
- Audio: NAudio (or similar)
- Process: System.Diagnostics.Process (PowerShell IO)
- Input: OpenTK keyboard events
- Assets: Bitmap font atlas (PNG), shaders, audio files

## 6. Testing & Validation
- Terminal matches PowerShell 7 command handling
- Pixel-accurate character rendering with effects
- Stable performance during continuous I/O
- Resizing does not cause crashes/artifacts
- Diagnostic/test mode for timing/buffer stats

## 7. Extensibility (Future)
- Multiple tabs
- Theme system (CRT styles)
- ANSI color output
- Scripting/macros
- Export buffer (image/text)
- Retro file browser
