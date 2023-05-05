using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Build))]
public class BuildEditor : Editor
{
    Build source;

    private void OnEnable()
    {
        source = target as Build;

        /*string[] paths = new string[2];
        paths[0] = "Assets/Scriptables/Items/";
        paths[1] = "Assets/Scriptables/Recipes/";
        Debug.Log(AssetDatabase.FindAssets("", paths).Length);*/
    }

    public override void OnInspectorGUI()
    {
        source.validMat = (Material)EditorGUILayout.ObjectField("Valid Material", source.validMat, typeof(Material), false);
        source.invalidMat = (Material)EditorGUILayout.ObjectField("Invalid Material", source.invalidMat, typeof(Material), false);

        source.realObject = (GameObject)EditorGUILayout.ObjectField("Real Object", source.realObject, typeof(GameObject), true);

        source.item = (Item)EditorGUILayout.ObjectField("Item", source.item, typeof(Item), true);

        source.xRange = EditorGUILayout.IntField("X", source.xRange);
        source.zRange = EditorGUILayout.IntField("Z", source.zRange);
        
        for (int z = source.zRange; z > - (source.zRange + 1); z--)
        {
            GUILayout.BeginHorizontal();
            for (int x = -source.xRange; x < source.xRange + 1; x++)
            {
                ShowButton(x, z);
            }
            GUILayout.EndHorizontal();
        }
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
