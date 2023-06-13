using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    //Variable reset à chaque fois que l'inspector se réouvre (exple : on clique sur un autre item)
    Item source;
    GUIStyle style;
    Color32 textTitleColor = Color.white;

    private void OnEnable()
    {
        source = target as Item;
    }

    //Update
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        style = new GUIStyle()
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,

            normal = new GUIStyleState()
            {
                textColor = textTitleColor,
                background = Texture2D.grayTexture
            },
        };

        source.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(source.itemType, style);

        switch (source.itemType)
        {
            case Item.ItemType.TOOL:
                ShowToolUI();
                break;
            case Item.ItemType.RESOURCE:
                ShowResourceUI();
                break;
            case Item.ItemType.BUILD:
                ShowBuildUI();
                break;
            default:
                break;
        }
        EditorUtility.SetDirty(source);
    }


    public void ShowToolUI()
    {
        GUILayout.BeginHorizontal("box");
        //EditorGUILayout.LabelField("Prefab");
        source.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", source.Prefab, typeof(GameObject), false);
        GUILayout.EndHorizontal();

        //EditorGUILayout.Space(20f);
        GUILayout.BeginHorizontal("box");
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("Sprite", GUILayout.Height(50));
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        source.Sprite = (Sprite)EditorGUILayout.ObjectField(source.Sprite, typeof(Sprite), false);
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        Rect r = new Rect(Screen.width - 40, GUILayoutUtility.GetLastRect().y, 50, 50);

        if (GUILayout.Button(AssetPreview.GetAssetPreview(source.Sprite), GUILayout.Width(r.width), GUILayout.Height(r.height)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(source.Sprite, true, "", 0);
        }

        if (EditorGUIUtility.GetObjectPickerObject() != null)
        {
            if (EditorGUIUtility.GetObjectPickerObject() as Sprite && EditorGUIUtility.GetObjectPickerControlID() == 0)
            {
                EditorGUIUtility.GetObjectPickerControlID();
                source.Sprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();
            }

        }

        //EditorGUI.DrawPreviewTexture(r, AssetPreview.GetAssetPreview(source.Sprite));
        GUILayout.EndHorizontal();
        source.Name = EditorGUILayout.TextField("Name", source.Name);
        source.Description = EditorGUILayout.TextField("Description", source.Description);
        source.Amount = 1;
        source.Placeable = false;

        /*string assetPath = AssetDatabase.GetAssetPath(source.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, source.Name);
        AssetDatabase.SaveAssets();*/

        textTitleColor = new Color(0.9f,0f,0f);
    }

    public void ShowResourceUI()
    {
        GUILayout.BeginHorizontal("box");
        //EditorGUILayout.LabelField("Prefab");
        source.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", source.Prefab, typeof(GameObject), false);
        GUILayout.EndHorizontal();

        //EditorGUILayout.Space(20f);
        GUILayout.BeginHorizontal("box");
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("Sprite", GUILayout.Height(50));
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        source.Sprite = (Sprite)EditorGUILayout.ObjectField(source.Sprite, typeof(Sprite), false);
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        Rect r = new Rect(Screen.width - 40, GUILayoutUtility.GetLastRect().y, 50, 50);

        if (GUILayout.Button(AssetPreview.GetAssetPreview(source.Sprite), GUILayout.Width(r.width), GUILayout.Height(r.height)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(source.Sprite, true, "", 0);
        }

        if (EditorGUIUtility.GetObjectPickerObject() != null)
        {
            if (EditorGUIUtility.GetObjectPickerObject() as Sprite && EditorGUIUtility.GetObjectPickerControlID() == 0)
            {
                EditorGUIUtility.GetObjectPickerControlID();
                source.Sprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();
            }

        }

        //EditorGUI.DrawPreviewTexture(r, AssetPreview.GetAssetPreview(source.Sprite));
        GUILayout.EndHorizontal();
        source.Name = EditorGUILayout.TextField("Name", source.Name);
        source.Description = EditorGUILayout.TextField("Description", source.Description);
        source.Amount = EditorGUILayout.IntField("Amount", source.Amount);
        source.Placeable = false;

        textTitleColor = new Color(0f, 0.9f, 0f);

    }
    public void ShowBuildUI()
    {
        GUILayout.BeginHorizontal("box");
        //EditorGUILayout.LabelField("Prefab");
        source.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab",source.Prefab, typeof(GameObject), false);
        GUILayout.EndHorizontal();

        //EditorGUILayout.Space(20f);
        GUILayout.BeginHorizontal("box");
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("Sprite", GUILayout.Height(50));
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        source.Sprite = (Sprite)EditorGUILayout.ObjectField(source.Sprite, typeof(Sprite), false);
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        Rect r = new Rect(Screen.width - 40, GUILayoutUtility.GetLastRect().y, 50, 50);

        if (GUILayout.Button(AssetPreview.GetAssetPreview(source.Sprite), GUILayout.Width(r.width), GUILayout.Height(r.height)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(source.Sprite,true,"",0);
        }

        if (EditorGUIUtility.GetObjectPickerObject() != null)
        {
            if (EditorGUIUtility.GetObjectPickerObject() as Sprite && EditorGUIUtility.GetObjectPickerControlID() == 0)
            {
                EditorGUIUtility.GetObjectPickerControlID();
                source.Sprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();
            }
            
        }
            
        //EditorGUI.DrawPreviewTexture(r, AssetPreview.GetAssetPreview(source.Sprite));
        GUILayout.EndHorizontal();
        source.Name = EditorGUILayout.TextField("Name", source.Name);
        source.Description = EditorGUILayout.TextField("Description", source.Description);
        source.Amount = EditorGUILayout.IntField("Amount", source.Amount);
        source.Placeable = EditorGUILayout.Toggle("Placeable", source.Placeable);

        source.recipe = (Recipe)EditorGUILayout.ObjectField("Recipe", source.recipe, typeof(Recipe), false);

        textTitleColor = new Color(0f, 0f, 0.9f);
    }

    //Draw sur la scene
    /*private void OnSceneGUI()
    {
        
    }*/
}
