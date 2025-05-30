# Copilot Collaboration Instructions

This document describes the workflow and collaboration process for working with GitHub Copilot on this project.

## Workflow Overview

1. **Task Request**
   - The user (project owner) requests a specific task or feature.

2. **Implementation & Verification**
   - Copilot implements the requested task.
   - Copilot verifies that the code compiles, is well-structured, and follows best practices.

3. **User Review**
   - Copilot asks the user to verify that the implementation works as expected.

4. **Confirmation & Versioning**
   - When the user confirms the task is working as expected:
     - Copilot bumps the version number in the software (using semantic versioning).
     - Copilot adds a summary of the change to `CHANGELOG.md`.

5. **Repeat**
   - The process repeats for each new task or feature.

## Project Structure Guidelines

- All source code should reside in the `/src` directory (e.g., `/src/CoolRetroPowershellTerm`).
- Solution file (`.sln`) should be in the project root.
- Documentation is kept in `/docs`.
- Assets (fonts, shaders, audio) should be placed in `/assets`.
- GitHub and workflow configuration in `/.github`.
- Changelog in `/CHANGELOG.md`.

**Example Structure:**
```
/ (root)
  /src/CoolRetroPowershellTerm
  /docs
  /assets
  /.github
  CHANGELOG.md
  README.md
  cool-retro-powershell-term.sln
```

---

_Last updated: 2025-05-30_
