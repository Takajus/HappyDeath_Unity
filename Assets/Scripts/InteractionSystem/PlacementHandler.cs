using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHandler : BaseHandler
{
    public override bool IsInteracting { get => objectToPlace != null; }
    GameObject objectToPlace;
    int rotation;

    [SerializeField] GameObject tempPrefab;
    [SerializeField] Transform buildsParent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsInteracting)
                ClearHandler();
            else
                GiveObject(tempPrefab);
        }

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

        rotation = 0;
    }

    public void GiveObject(GameObject ob)
    {
        if (buildsParent)
            objectToPlace = Instantiate(tempPrefab, buildsParent);
        else
            objectToPlace = Instantiate(tempPrefab);
    }
    
    protected override void HoverTarget(GameObject target)
    {
        if (target)
        {
            target.SetActive(true);
            objectToPlace.transform.position = target.transform.position;
        }
        else
            target.SetActive(false);
    }

    protected override void SelectTarget(GameObject target)
    {
        if (target)
            objectToPlace = null;
    }

    protected override void UnHoverTarget(GameObject target) { }
    protected override void UnSelectTarget(GameObject target) { }

    void RotateTarget()
    {
        if (Input.GetKeyDown(KeyCode.R) && IsInteracting)
        {
            rotation += 90;

            if (rotation == 360)
                rotation = 0;

            //objectToPlace.transform.Rotate(new Vector3(0, 1, 0), rotation, Space.World);
            objectToPlace.transform.eulerAngles = new Vector3(0, rotation, 0);
        }
    }
}
