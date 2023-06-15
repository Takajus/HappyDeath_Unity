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
        foreach (var renderer in previewMeshes)
        {
            List<Material> materials = new List<Material>();

            foreach (var material in renderer.materials)
            {
                materials.Add(material);
            }

            materials.Add(InteractionManager.Instance.GetHoverMat());
            renderer.materials = materials.ToArray();
        }
    }

    public void UnHover()
    {
        foreach (var renderer in previewMeshes)
        {
            List<Material> materials = new List<Material>();

            foreach (var material in renderer.materials)
            {
                materials.Add(material);
            }

            materials.RemoveAt(materials.Count - 1);
            renderer.materials = materials.ToArray();
        }
    }

    public void Interact()
    {
        
    }

    public void EndInteract()
    {
        
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
}