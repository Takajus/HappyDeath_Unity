using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    [SerializeField] Transform topRightCorner;
    [SerializeField] Transform bottomLeftCorner;
    [SerializeField] Transform gridParent;
    [SerializeField] float tileSize = 1;
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

        rowNumber = Mathf.RoundToInt(Mathf.Abs(bottomLeftCorner.transform.position.z - topRightCorner.transform.position.z) / tileSize);
        columnNumber = Mathf.RoundToInt(Mathf.Abs(bottomLeftCorner.transform.position.x - topRightCorner.transform.position.x) / tileSize);

        Tile newTile;

        for (int j = 0; j < rowNumber; j++)
        {
            for (int k = 0; k < columnNumber; k++)
            {
                positionToPlaceTile = bottomLeftCorner.transform.position + new Vector3(tileSize * k, 0, tileSize * j);
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

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    GridManager source;

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
}
