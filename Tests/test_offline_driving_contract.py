import unittest
from pathlib import Path


PROJECT_ROOT = Path(__file__).resolve().parents[1]
ASSETS_ROOT = PROJECT_ROOT / "Assets"


class OfflineDrivingSliceContractTests(unittest.TestCase):
    def test_offline_slice_has_scene_and_prefab_assets(self):
        scenes = list((ASSETS_ROOT / "Scenes").glob("*.unity"))
        prefabs = list((ASSETS_ROOT / "Prefabs").glob("*.prefab"))

        self.assertTrue(
            scenes,
            "Offline-driving slice requires at least one Unity scene under Assets/Scenes.",
        )
        self.assertGreaterEqual(
            len(prefabs),
            2,
            "Offline-driving slice requires a track and car prefabs under Assets/Prefabs.",
        )

    def test_offline_slice_scripts_cover_driving_race_bots_and_input(self):
        scripts = {
            script.stem.lower()
            for script in (ASSETS_ROOT / "Scripts").rglob("*.cs")
        }

        required_roles = {
            "driving": ("vehicle", "car", "drive"),
            "race": ("race",),
            "bots": ("bot", "ai"),
            "input": ("input", "control"),
        }
        for role, name_fragments in required_roles.items():
            with self.subTest(role=role):
                self.assertTrue(
                    any(
                        any(fragment in script_name for fragment in name_fragments)
                        for script_name in scripts
                    ),
                    f"Offline-driving slice is missing a {role} script under Assets/Scripts.",
                )


if __name__ == "__main__":
    unittest.main()
