# Offroad Racing

Minimal Unity PC offline slice for the approved off-road racing prototype.
It contains a generated driving scene, car/track/bot prefabs, and focused
gameplay scripts. Network behavior remains out of scope for this checkpoint.
The offline loop starts immediately, fills open grid slots with bots, supports
third-person driving and recovery, and awards the mud-runner unlock on finish.

## Baseline

- **Unity editor:** 2022.3.62f3 LTS
- **Input:** Unity Input System 1.7.0, selected as the active input handler
- **Networking packages:** Netcode for GameObjects 1.5.2, Lobby declaration,
  Relay 1.2.0 (no network behavior is implemented)
- **Target:** PC/Standalone

## Prerequisites

Install Unity Hub and the exact Unity 2022.3.62f3 LTS editor, including the
Windows PC build support module. Open this repository as a Unity project and
allow Package Manager to resolve `Packages/manifest.json`.

The Lobby dependency is declared as an offline package placeholder so the
project and tests resolve without network access. Live Relay/Lobby validation
requires a Unity Gaming Services project, project ID and credentials configured
outside this repository, plus internet access and two reachable PC instances.
Do not commit credentials.

## Validation status

- **PASS — repository contract:** bootstrap files, package declarations,
  scene/prefab assets, and prerequisite documentation are checked by
  `python -m unittest discover -s Tests -p "test_*.py"`.
- **Unity validation:** run EditMode and PlayMode tests with the exact editor
  listed above after the initial asset import; live network validation is out
  of scope.
- **BLOCKED — Unity execution:** batch test output remains unavailable until
  the editor completes its first import in this checkout.
