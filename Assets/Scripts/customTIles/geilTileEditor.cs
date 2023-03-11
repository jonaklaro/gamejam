using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(customTileGeil))]
public class geilTileEditor : Editor
{
    string path = "";
    public override void OnInspectorGUI()
    {
        customTileGeil script = (customTileGeil)target;
        EditorUtility.SetDirty(script);

        script.sprite = (Sprite)EditorGUILayout.ObjectField("sprite", script.sprite, typeof(Sprite), false);

        script.m_size = EditorGUILayout.Vector2IntField("Size", script.m_size);

        script.offset = EditorGUILayout.Vector2IntField("Offset", script.offset);

        path = EditorGUILayout.TextField("path", path);

        if (GUILayout.Button("load Tiles"))
        {
            Debug.Log(path);
            script.loadTiles(path);
        }
    }

}
#endif
