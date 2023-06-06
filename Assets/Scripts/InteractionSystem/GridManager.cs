using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    public static GridManager Instance { get { if (instance == null) instance = FindObjectOfType<GridManager>(); return instance; } }

    [SerializeField] GameObject gridTile;
    [SerializeField] Dictionary<string, Tile> gridTiles = new Dictionary<string, Tile>();

    [Serializable]
    public struct Tiles
    {
        public string name;
        public Tile tile;

        public Tiles(string name, Tile tile)
        {
            this.name = name;
            this.tile = tile;
        }
    }
    public List<Tiles> tiles = new List<Tiles>();

    public float tileSize;
    public Vector3 topRightCorner;
    public Vector3 bottomLeftCorner;
    [SerializeField] Transform gridParent;
    Vector3 positionToPlaceTile;
    int rowNumber;
    int columnNumber;

    void Awake()
    {
        foreach (var item in tiles)
            if (item.tile != null)
                gridTiles.Add(item.name, item.tile);
    }

    public void CreateGrid()
    {
        tiles.Clear();

        for (int i = gridParent.transform.childCount - 1; i > 0; i--)
            DestroyImmediate(gridParent.GetChild(i).gameObject);

        rowNumber = Mathf.RoundToInt(Mathf.Abs(bottomLeftCorner.z - topRightCorner.z) / tileSize);
        columnNumber = Mathf.RoundToInt(Mathf.Abs(bottomLeftCorner.x - topRightCorner.x) / tileSize);

        Tile newTile;

        for (int j = 0; j < rowNumber; j++)
        {
            for (int k = 0; k < columnNumber; k++)
            {
                positionToPlaceTile = bottomLeftCorner + new Vector3(tileSize * k + tileSize/2, 0, tileSize * j + tileSize / 2);
                newTile = Instantiate(gridTile, positionToPlaceTile, Quaternion.identity, gridParent).GetComponent<Tile>();
                newTile.transform.localScale = new Vector3(tileSize, newTile.transform.localScale.y, tileSize);
                newTile.x = tileSize * k;
                newTile.z = tileSize * j;
                tiles.Add(new Tiles(tileSize * k + ";" + tileSize * j, newTile));
            }
        }
    }

    public void ClearGrid()
    {
        if (Application.isPlaying)
            return;

        tiles.Clear();

        for (int i = gridParent.transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(gridParent.GetChild(i).gameObject);
    }

    public List<Tile> GetNeighbors(Tile tile)
    {
        List<Tile> neighbors = new List<Tile>();

        if (gridTiles.ContainsKey((tile.x - tileSize) + ";" + tile.z))
            neighbors.Add(gridTiles[(tile.x - tileSize) + ";" + tile.z]);

        if (gridTiles.ContainsKey(tile.x + ";" + (tile.z - tileSize)))
            neighbors.Add(gridTiles[tile.x + ";" + (tile.z - tileSize)]);

        if (gridTiles.ContainsKey((tile.x + tileSize) + ";" + tile.z))
            neighbors.Add(gridTiles[(tile.x + tileSize) + ";" + tile.z]);

        if (gridTiles.ContainsKey(tile.x + ";" + (tile.z + tileSize)))
            neighbors.Add(gridTiles[tile.x + ";" + (tile.z + tileSize)]);

        return neighbors;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    GridManager source;
    bool test;
    private void OnEnable()
    {
        source = target as GridManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Grid"))
            source.CreateGrid();

        if (GUILayout.Button("Clear Grid"))
            source.ClearGrid();
    }

    private void OnSceneGUI()
    {
        GizmoTest(GizmoType.Selected);
    }

    // Custom Gizmos, Create as many as you'd like
    [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Selected)]
    //TODO: Replace first argument with the type you are editing
    void GizmoTest(GizmoType aGizmoType)
    {
        source.bottomLeftCorner = Handles.PositionHandle(source.bottomLeftCorner, Quaternion.identity);
        source.topRightCorner = Handles.PositionHandle(source.topRightCorner, Quaternion.identity);

        if (source.topRightCorner.x < source.bottomLeftCorner.x + source.tileSize)
            source.topRightCorner.x = source.bottomLeftCorner.x + source.tileSize;

        if (source.topRightCorner.z < source.bottomLeftCorner.z + source.tileSize)
            source.topRightCorner.z = source.bottomLeftCorner.z + source.tileSize;

        source.bottomLeftCorner.x = Mathf.Round(source.bottomLeftCorner.x / source.tileSize) * source.tileSize;
        source.bottomLeftCorner.z = Mathf.Round(source.bottomLeftCorner.z / source.tileSize) * source.tileSize;

        source.topRightCorner.x = Mathf.Round(source.topRightCorner.x / source.tileSize) * source.tileSize;
        source.topRightCorner.z = Mathf.Round(source.topRightCorner.z / source.tileSize) * source.tileSize;
    }
}
#endif
