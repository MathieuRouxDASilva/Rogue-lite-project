using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BSPGenerator))]
public class BPSGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BSPGenerator gen = (BSPGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            gen.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            gen.Clear();
        }
    }
}