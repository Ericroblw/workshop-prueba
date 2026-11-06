# Offroad Racing

Minimal Unity PC project bootstrap for the approved off-road racing prototype.
This checkpoint contains project configuration only; it does not implement
gameplay, scenes, networking behavior, or fake tests.

## Baseline

- **Unity editor:** 2022.3.62f3 LTS
- **Input:** Unity Input System 1.7.0, selected as the active input handler
- **Networking packages:** Netcode for GameObjects 1.5.2, Lobby 1.1.1, Relay 1.2.0
- **Target:** PC/Standalone

## Prerequisites

Install Unity Hub and the exact Unity 2022.3.62f3 LTS editor, including the
Windows PC build support module. Open this repository as a Unity project and
allow Package Manager to resolve `Packages/manifest.json`.

Relay and Lobby validation also requires a Unity Gaming Services project,
project ID and credentials configured outside this repository, plus internet
access and two reachable PC instances. Do not commit credentials.

## Validation status

- **PASS — repository contract:** bootstrap files, package declarations, and
  prerequisite documentation are checked by `python -m unittest discover -s
  Tests -p "test_*.py"`.
- **BLOCKED — Unity execution:** EditMode/PlayMode and manual validation cannot
  run until the exact Unity editor is installed. No Unity execution or
  gameplay/network behavior is claimed by this bootstrap.
