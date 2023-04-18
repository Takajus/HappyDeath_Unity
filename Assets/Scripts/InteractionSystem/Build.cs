using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Material validMat;
    public Material invalidMat;

    [SerializeField] GameObject realObject;
    [SerializeField] MeshRenderer notRealMesh;
    [SerializeField] Item item;

    [System.Serializable]
    public struct TileDetection
    {
        public int x;
        public int z;

        public TileDetection(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    }

    [SerializeField] List<TileDetection> tileDetectionList = new List<TileDetection>();

    private void Start()
    {
        notRealMesh = GetComponent<MeshRenderer>();
    }

    public void CheckPlaceability()
    {
        if (IsPlaceable())
            notRealMesh.material = validMat;
        else
            notRealMesh.material = invalidMat;
    }

    public bool IsPlaceable()
    {
        if (GetOverlappedTiles().Count < tileDetectionList.Count)
            return false;

        foreach (var tile in GetOverlappedTiles())
            if (tile.isOccupied)
                return false;

        return true;
    }

    private HashSet<Tile> GetOverlappedTiles()
    {
        HashSet<Tile> tiles = new HashSet<Tile>();

        foreach (var tileDetection in tileDetectionList)
        {
            Vector3 relativePosition = transform.parent.TransformDirection(new Vector3(tileDetection.x, 0, tileDetection.z));
            Collider[] hitColliders = Physics.OverlapBox(transform.parent.position + relativePosition, Vector3.one / 3, Quaternion.identity, ~7);

            foreach (var hit in hitColliders)
                if (hit.GetComponent<Tile>())
                    tiles.Add(hit.GetComponent<Tile>());
        }

        return tiles;
    }

    public void Innit()
    {
        realObject.SetActive(true);
        gameObject.SetActive(false);
        InventoryManager.Instance.RemoveItem(item);

        foreach (var tile in GetOverlappedTiles())
        {
            tile.isOccupied = true;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var tileDetection in tileDetectionList)
        {
            Vector3 relativePosition = transform.parent.TransformDirection(new Vector3(tileDetection.x, 0, tileDetection.z));
            Gizmos.DrawWireCube(transform.parent.position + relativePosition, Vector3.one / 3);
        }
    }
}
