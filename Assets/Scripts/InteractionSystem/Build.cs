using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Material validMat;
    public Material invalidMat;

    [SerializeField] GameObject realObject;
    [SerializeField] MeshRenderer realMesh;

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
        realMesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (IsPlaceable())
            realMesh.material = validMat;
        else
            realMesh.material = invalidMat;
    }

    public bool IsPlaceable()
    {
        foreach (var tileDetection in tileDetectionList)
        {
            bool hasValidTile = false;

            Collider[] hitColliders = Physics.OverlapBox(transform.parent.transform.position + transform.parent.TransformDirection(new Vector3(tileDetection.x, 0, tileDetection.z)), Vector3.one / 3, Quaternion.identity, ~7);

            Debug.Log(hitColliders.Length);

            foreach (var hit in hitColliders)
            {
                if (hit.GetComponent<Tile>())
                {
                    hasValidTile = true;

                    if (hit.GetComponent<Tile>().isOccupied)
                        hasValidTile = false;
                }
            }

            if (!hasValidTile)
                return false;
        }

        return true;
    }

    public void Innit()
    {
        realObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        foreach (var tileDetection in tileDetectionList)
        {
            Gizmos.DrawWireCube(transform.parent.transform.position + transform.parent.TransformDirection(new Vector3(tileDetection.x, 0, tileDetection.z)), Vector3.one / 3);
        }
    }
}
