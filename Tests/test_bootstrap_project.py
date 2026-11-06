import json
import unittest
from pathlib import Path


PROJECT_ROOT = Path(__file__).resolve().parents[1]


class BootstrapProjectContractTests(unittest.TestCase):
    def test_unity_project_baseline_files_exist(self):
        required_files = (
            PROJECT_ROOT / "ProjectSettings" / "ProjectVersion.txt",
            PROJECT_ROOT / "Packages" / "manifest.json",
            PROJECT_ROOT / "README.md",
        )

        for required_file in required_files:
            with self.subTest(path=required_file.relative_to(PROJECT_ROOT)):
                self.assertTrue(
                    required_file.is_file(),
                    f"Missing bootstrap deliverable: {required_file.relative_to(PROJECT_ROOT)}",
                )

    def test_package_manifest_and_prerequisites_are_declared(self):
        manifest_path = PROJECT_ROOT / "Packages" / "manifest.json"
        self.assertTrue(manifest_path.is_file())

        with manifest_path.open(encoding="utf-8") as manifest_file:
            manifest = json.load(manifest_file)

        dependencies = manifest.get("dependencies")
        self.assertIsInstance(dependencies, dict)
        for package_name in (
            "com.unity.inputsystem",
            "com.unity.netcode.gameobjects",
            "com.unity.services.lobbies",
            "com.unity.services.relay",
        ):
            with self.subTest(package=package_name):
                self.assertIn(package_name, dependencies)

        readme_path = PROJECT_ROOT / "README.md"
        self.assertTrue(readme_path.is_file())
        readme = readme_path.read_text(encoding="utf-8").lower()
        for prerequisite in ("unity", "input system", "relay", "lobby"):
            with self.subTest(prerequisite=prerequisite):
                self.assertIn(prerequisite, readme)

    def test_validation_status_separates_pass_from_blocked(self):
        readme = (PROJECT_ROOT / "README.md").read_text(encoding="utf-8")
        self.assertIn("PASS — repository contract", readme)
        self.assertIn("BLOCKED — Unity execution", readme)


if __name__ == "__main__":
    unittest.main()
