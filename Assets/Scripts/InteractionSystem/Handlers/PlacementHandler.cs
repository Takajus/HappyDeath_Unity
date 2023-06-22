using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHandler : BaseHandler
{
    public override bool IsInteracting { get => objectToPlace != null; }
    GameObject objectToPlace;
    int rotation = 180;

    [SerializeField] GameObject tempPrefab;
    [SerializeField] Transform buildsParent;

    void Update()
    {
        RotateTarget();
    }

    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<Tile>() != null)
            return true;

        return false;
    }

    public override void ClearHandler()
    {
        if (objectToPlace)
            Destroy(objectToPlace);

        rotation = 180;
    }

    public override void InitializeHandler()
    {
        GiveObject(InventoryManager.HeldItem?.Prefab);
        InventoryManager.Instance.HeldItemChanged();
    }

    public void GiveObject(GameObject ob)
    {
        if (buildsParent)
            objectToPlace = Instantiate(ob, buildsParent);
        else
            objectToPlace = Instantiate(ob);

        objectToPlace.transform.eulerAngles = new Vector3(0, rotation, 0);
        objectToPlace.SetActive(false);
    }
    
    protected override void HoverTarget(GameObject target)
    {
        if (target)
        {
            objectToPlace.SetActive(true);
            objectToPlace.transform.position = target.transform.position;
            objectToPlace.GetComponent<Build>().CheckPlaceability();
        }
        else
            objectToPlace.SetActive(false);
    }

    protected override void SelectTarget(GameObject target)
    {
        if (target && objectToPlace.GetComponent<Build>().IsPlaceable())
        {
            objectToPlace.GetComponent<Build>().Innit();
            objectToPlace = null;
            InventoryManager.HeldItem = null;
            InventoryManager.Instance.HeldItemChanged();
        }
    }

    void RotateTarget()
    {
        if (Input.GetKeyDown(KeyCode.R) && IsInteracting)
        {
            rotation += 90;

            if (rotation == 360)
                rotation = 0;

            objectToPlace.transform.eulerAngles = new Vector3(0, rotation, 0);

            objectToPlace.GetComponent<Build>().CheckPlaceability();
        }
    }
}
