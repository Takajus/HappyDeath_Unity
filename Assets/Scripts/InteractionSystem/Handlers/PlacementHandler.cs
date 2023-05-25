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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsInteracting)
                ClearHandler();
            else
                GiveObject(tempPrefab);
        }
#endif
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

    public void GiveObject(GameObject ob)
    {
        if (buildsParent)
            objectToPlace = Instantiate(tempPrefab, buildsParent);
        else
            objectToPlace = Instantiate(tempPrefab);

        objectToPlace.transform.eulerAngles = new Vector3(0, rotation, 0);
        objectToPlace.SetActive(false);
    }
    
    protected override void HoverTarget(GameObject target)
    {
        if (target)
        {
            objectToPlace.SetActive(true);
            objectToPlace.transform.position = target.transform.position;
            objectToPlace.GetComponentInChildren<Build>().CheckPlaceability();
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
        }
    }
}
