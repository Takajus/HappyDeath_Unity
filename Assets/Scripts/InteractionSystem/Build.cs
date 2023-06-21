using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Build : MonoBehaviour, IInteractable
{
    public Material validMat;
    public Material invalidMat;
    public GameObject actualObject;
    public GameObject previewObject;
    public Item item;

    public int xRange = 2;
    public int zRange = 2;
    private List<MeshRenderer> previewMeshes = new List<MeshRenderer>();
    HashSet<Tile> tiles = new HashSet<Tile>();
    public List<TileDetection> tileDetectionList = new List<TileDetection>();

    [System.Serializable]
    public struct TileDetection
    {
        public int x;
        public int z;
        public bool needDugTile;

        public TileDetection(int x, int z, bool needDug = false)
        {
            this.x = x;
            this.z = z;
            this.needDugTile = needDug;
        }
    }

    private void Start()
    {
        previewMeshes = previewObject.GetComponentsInChildren<MeshRenderer>().ToList();
    }

    public void CheckPlaceability()
    {
        if (IsPlaceable())
        {
            for (int i = 0; i < previewMeshes.Count; i++)
            {
                previewMeshes[i].material = validMat;
            }
        }
        else
        {
            for (int i = 0; i < previewMeshes.Count; i++)
            {
                previewMeshes[i].material = invalidMat;
            }
        }
    }

    public bool IsPlaceable()
    {
        GetOverlappedTiles();

        AudioManager.Instance.Place_Tombstone.Post(gameObject);
        if (tiles.Count < tileDetectionList.Count)
            return false;

        foreach (var tile in tiles)
            if (tile.isOccupied)
                return false;

        return true;
    }

    private void GetOverlappedTiles()
    {
        HashSet<Tile> tiles = new HashSet<Tile>();

        foreach (var tileDetection in tileDetectionList)
        {
            Vector3 relativePosition = transform.TransformDirection(new Vector3(tileDetection.x, 0, tileDetection.z));
            Collider[] hitColliders = Physics.OverlapBox(transform.position + relativePosition, Vector3.one / 3, Quaternion.identity, ~7);

            foreach (var hit in hitColliders)
                if (hit.TryGetComponent(out Tile tile))
                    if (tile.IsDug == tileDetection.needDugTile)
                        tiles.Add(tile);
        }

        this.tiles = tiles;
    }

    public void Innit()
    {
        actualObject.SetActive(true);
        previewObject.SetActive(false);
        InventoryManager.Instance.RemoveItem(item);

        foreach (var tile in tiles)
        {
            tile.isOccupied = true;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var tileDetection in tileDetectionList)
        {
            if (tileDetection.needDugTile)
                Gizmos.color = Color.yellow;
            else
                Gizmos.color = Color.green;

            Vector3 relativePosition = transform.TransformDirection(new Vector3(tileDetection.x, 0, tileDetection.z));
            Gizmos.DrawWireCube(transform.position + relativePosition, Vector3.one / 3);
        }
    }

    public void Hover()
    {

    }

    public void UnHover()
    {

    }

    public void Interact()
    {
        GetOverlappedTiles();
        foreach (var tile in tiles)
            tile.isOccupied = false;

        if (item.recipe.ingredient1.ingredientType != null)
            InventoryManager.Instance.AddItem(item.recipe.ingredient1.ingredientType, (int)(item.recipe.ingredient1.IngredientAmount / 0.75f));
        if (item.recipe.ingredient2.ingredientType != null)
            InventoryManager.Instance.AddItem(item.recipe.ingredient2.ingredientType, (int)(item.recipe.ingredient2.IngredientAmount / 0.75f));
        if (item.recipe.ingredient3.ingredientType != null)
            InventoryManager.Instance.AddItem(item.recipe.ingredient3.ingredientType, (int)(item.recipe.ingredient3.IngredientAmount / 0.75f));

        GetComponentInChildren<Tomb>()?.ExtractNPC();

        Destroy(gameObject);
    }

    public void EndInteract()
    {
        
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
}