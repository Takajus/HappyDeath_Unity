using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject gridTile;
    List<GameObject> gridTiles = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateGrid()
    {

    }
}

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
