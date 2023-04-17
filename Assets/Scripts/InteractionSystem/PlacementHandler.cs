using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHandler : BaseHandler
{
    public bool IsPlacing { get => objectToPlace != null; }
    [SerializeField] GameObject objectToPlace;

    [SerializeField] GameObject tempPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsPlacing)
                ClearHandler();
            else
                GiveObject(tempPrefab);
        }
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
    }

    public void GiveObject(GameObject ob)
    {
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
        objectToPlace = null;
    }

    protected override void UnHoverTarget(GameObject target) { }
    protected override void UnSelectTarget(GameObject target) { }
}
