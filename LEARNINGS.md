# Learnings

## Rules

No promoted rules yet.

## Log

### 2026-11-06 · checkpoint videojuego-carreras-offroad#1 · behavior

- **Finding:** Repository bootstrap checks passed, but Unity EditMode execution was blocked because the Unity Editor is unavailable on `PATH`.
- **Root cause:** Missing external Unity Editor installation.
- **Fix:** Documented explicit PASS versus BLOCKED validation status in the bootstrap README and checkpoint state.
- **Lesson:** External-tool absence must remain BLOCKED, never reported as passed or hidden as a test failure.
