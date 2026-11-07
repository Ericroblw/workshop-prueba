using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class OfflineBuild
{
    public static void Build()
    {
        var outputPath = Path.Combine("Build", "OffroadPrototype.exe");
        Directory.CreateDirectory("Build");
        PlayerSettings.SetScriptingBackend(
            BuildTargetGroup.Standalone,
            ScriptingImplementation.Mono2x);

        var report = BuildPipeline.BuildPlayer(
            new[] { "Assets/Scenes/OfflineDriving.unity" },
            outputPath,
            BuildTarget.StandaloneWindows64,
            BuildOptions.None);

        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new System.Exception(
                $"Windows build failed: {report.summary.result} ({report.summary.totalErrors} errors)");
        }

        Debug.Log($"Windows build succeeded: {outputPath} ({report.summary.totalSize} bytes)");
    }
}
