using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Build))]
public class BuildEditor : Editor
{
    Build source;

    private void OnEnable()
    {
        source = target as Build;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        source.validMat = (Material)EditorGUILayout.ObjectField("Valid Material", source.validMat, typeof(Material), false);
        source.invalidMat = (Material)EditorGUILayout.ObjectField("Invalid Material", source.invalidMat, typeof(Material), false);

        source.actualObject = (GameObject)EditorGUILayout.ObjectField("Actual Object", source.actualObject, typeof(GameObject), true);
        source.previewObject = (GameObject)EditorGUILayout.ObjectField("Preview Object", source.previewObject, typeof(GameObject), true);
        
        source.item = (Item)EditorGUILayout.ObjectField("Item", source.item, typeof(Item), true);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(source);
        }
        EditorGUILayout.Space();


        var style = new GUIStyle("window");
        style.padding = new RectOffset();
        EditorGUILayout.BeginVertical(style);
        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        GUILayout.Label("Grid setup", new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter, fontSize = 20 });
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        source.xRange = EditorGUILayout.IntField("X", source.xRange);
        if (GUILayout.Button("-"))
        {
            source.xRange--;
            if (source.xRange < 0)
                source.xRange = 0;
            EditorUtility.SetDirty(source);
        }
        if (GUILayout.Button("+"))
        {
            source.xRange++;
            EditorUtility.SetDirty(source);
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        source.zRange = EditorGUILayout.IntField("Y", source.zRange);
        if (GUILayout.Button("-"))
        {
            source.zRange--;
            if (source.zRange < 0)
                source.zRange = 0;
            EditorUtility.SetDirty(source);
        }
        if (GUILayout.Button("+"))
        {
            source.zRange++;
            EditorUtility.SetDirty(source);
        }
        GUILayout.EndHorizontal();
        for (int z = source.zRange; z > - (source.zRange + 1); z--)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int x = -source.xRange; x < source.xRange + 1; x++)
            {
                ShowButton(x, z);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();
        GUILayout.EndVertical();
    }

    void ShowButton(int x, int z)
    {
        bool exists = source.tileDetectionList.Exists(e => e.x == x && e.z == z);
        bool needDug = false;
        int index = 0;

        if (exists)
        {
            index = source.tileDetectionList.FindIndex(e => e.x == x && e.z == z);
            needDug = source.tileDetectionList.Find(e => e.x == x && e.z == z).needDugTile;
        }

        if (exists && needDug)
            GUI.backgroundColor = Color.yellow;
        else if (exists)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.grey;

        string symbol = "";
        if (x == 0 && z == 0)
        {
            symbol = "+";
            GUI.backgroundColor /= 1.5f;
        }

        GUIStyle style = new GUIStyle("button") { fontSize = 25 };

        if (GUILayout.Button(symbol, style, GUILayout.Width(50), GUILayout.Height(50)))
        {
            if (exists && needDug)
                source.tileDetectionList.RemoveAt(index);
            else if (exists)
                source.tileDetectionList[index] = new Build.TileDetection(x, z, true);
            else
                source.tileDetectionList.Add(new Build.TileDetection(x, z));

            EditorUtility.SetDirty(source);
        }
    }
}
