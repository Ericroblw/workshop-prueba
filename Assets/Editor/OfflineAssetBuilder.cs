using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;
using UnityEngine;
using OffroadRacing.Gameplay;

public static class OfflineAssetBuilder
{
    [MenuItem("Tools/Offroad/Build Offline Slice")]
    public static void Build()
    {
        EnsureFolder("Assets/Scenes");
        EnsureFolder("Assets/Prefabs");
        if (!InternalEditorUtility.tags.Contains("Bot")) InternalEditorUtility.AddTag("Bot");

        var car = CreatePrefab("Assets/Prefabs/OffroadCar.prefab", PrimitiveType.Cube, new Vector3(1.6f, 0.6f, 3f));
        var track = CreatePrefab("Assets/Prefabs/OffroadTrack.prefab", PrimitiveType.Cube, new Vector3(18f, 0.2f, 80f));
        var bot = CreatePrefab("Assets/Prefabs/OffroadBot.prefab", PrimitiveType.Cube, new Vector3(1.6f, 0.6f, 3f));
        bot.tag = "Bot";
        PrefabUtility.SaveAsPrefabAsset(bot, "Assets/Prefabs/OffroadBot.prefab");

        var player = Instantiate(car, new Vector3(0f, 0.6f, 0f), Quaternion.identity);
        player.name = "PlayerCar";
        player.AddComponent<VehicleController>();
        var race = new GameObject("RaceController");
        race.AddComponent<RaceController>();
        var bots = new GameObject("BotManager");
        var manager = bots.AddComponent<BotManager>();
        var botField = typeof(BotManager).GetField("botPrefab", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        botField.SetValue(manager, bot);

        var trackObject = Instantiate(track, new Vector3(0f, 0f, 32f), Quaternion.identity);
        trackObject.name = "Track";
        var cameraObject = new GameObject("ThirdPersonCamera");
        cameraObject.tag = "MainCamera";
        var camera = cameraObject.AddComponent<Camera>();
        var follow = cameraObject.AddComponent<ThirdPersonCamera>();
        var targetField = typeof(ThirdPersonCamera).GetField("target", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        targetField.SetValue(follow, player.transform);
        camera.transform.position = new Vector3(0f, 4f, -7f);
        camera.transform.LookAt(player.transform);

        var lightObject = new GameObject("Sun");
        var light = lightObject.AddComponent<Light>();
        light.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        EditorSceneManager.SaveScene(EditorSceneManager.NewScene(NewSceneSetup.EmptyScene), "Assets/Scenes/OfflineDriving.unity");
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(trackObject);
        Object.DestroyImmediate(race);
        Object.DestroyImmediate(bots);
        Object.DestroyImmediate(cameraObject);
        Object.DestroyImmediate(lightObject);
        AssetDatabase.SaveAssets();
    }

    private static GameObject CreatePrefab(string path, PrimitiveType type, Vector3 scale)
    {
        var objectToSave = GameObject.CreatePrimitive(type);
        objectToSave.transform.localScale = scale;
        objectToSave.name = System.IO.Path.GetFileNameWithoutExtension(path);
        var result = PrefabUtility.SaveAsPrefabAsset(objectToSave, path);
        Object.DestroyImmediate(objectToSave);
        return result;
    }

    private static void EnsureFolder(string path)
    {
        if (!AssetDatabase.IsValidFolder(path)) AssetDatabase.CreateFolder(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileName(path));
    }
}
