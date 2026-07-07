using System.Linq;
using UnityEditor;
using UnityEngine;



public class ConfettiParticlesEditor : EditorWindow
{
    private ParticleSystem particleSystem;

    [MenuItem("Tools/Particle Sprite Assigner")]
    public static void ShowWindow()
    {
        GetWindow<ConfettiParticlesEditor>("Particle Sprite Assigner");
    }

    private void OnGUI()
    {
        GUILayout.Space(5);

        particleSystem = (ParticleSystem)EditorGUILayout.ObjectField(
            "Particle System",
            particleSystem,
            typeof(ParticleSystem),
            true);

        GUILayout.Space(10);

        EditorGUILayout.HelpBox(
            "1. Drag a Particle System here.\n" +
            "2. Select sprites in the Project window.\n" +
            "3. Click Assign.",
            MessageType.Info);

        GUILayout.Space(10);

        GUI.enabled = particleSystem != null;

        if (GUILayout.Button("Assign Selected Sprites", GUILayout.Height(35)))
        {
            AssignSprites();
        }

        GUI.enabled = true;
    }

    private void AssignSprites()
    {
        var sprites = Selection.objects
            .SelectMany(GetSpritesFromAsset)
            .OrderBy(s => s.name)
            .ToArray();

        if (sprites.Length == 0)
        {
            EditorUtility.DisplayDialog(
                "No Sprites Found",
                "Select one or more parent texture assets.",
                "OK");
            return;
        }

        Undo.RecordObject(particleSystem, "Assign Particle Sprites");

        var tsa = particleSystem.textureSheetAnimation;
        tsa.enabled = true;
        tsa.mode = ParticleSystemAnimationMode.Sprites;

        for (int i = tsa.spriteCount - 1; i >= 0; i--)
        {
            tsa.RemoveSprite(i);
        }
        
        // while (tsa.spriteCount > 0)
        //     tsa.RemoveSprite(0);

        foreach (var sprite in sprites)
            tsa.AddSprite(sprite);

        EditorUtility.SetDirty(particleSystem);

        Debug.Log($"Assigned {sprites.Length} sprites.");
    }

    private static Sprite[] GetSpritesFromAsset(Object asset)
    {
        string path = AssetDatabase.GetAssetPath(asset);

        return AssetDatabase
            .LoadAllAssetsAtPath(path)
            .OfType<Sprite>()
            .ToArray();
    }
}
