# Changelog

All notable changes to this project will be documented in this file.

## [0.4.0] - 2025-06-01
### Added
- Per-cell colored backgrounds: Each character cell can now have its own background color, supporting retro terminal color effects.
- Demo row with colored backgrounds to visually verify per-cell coloring.
- API for writing text with custom background color in `TextBuffer`.

### Changed
- Rendering loop now uses each cell's background color, not just a global color.

## [0.3.1] - 2025-05-31
### Changed
- Font rendering now uses double-size glyphs for a larger retro look.
- Terminal prompt and text now start at the true upper-left, with a top margin for aesthetics.
- Background for all terminal cells now matches the retro dark blue, eliminating black/transparent artifacts.
- Asset copying and font path resolution are robust for all build/run scenarios.

## [0.3.0] - 2025-05-30
### Added
- Basic rendering loop now displays a retro dark blue background using OpenGL clear color.
- Logging and error reporting for render loop verified.
- Added buffer swap to ensure rendered frame is visible.

## [0.2.0] - 2025-05-30
### Added
- Implemented `CharEntry` struct and a 2D `TextBuffer` class for terminal character storage.
- Simulated terminal output logic in `Program.cs` for early testing.

## [0.1.0] - 2025-05-30
### Added
- Initial project scaffolding: solution and main project in `/src`.
- OpenTK package added and minimal window created.
- Build and launch configuration for VS Code.
- Project structure and workflow documentation.
