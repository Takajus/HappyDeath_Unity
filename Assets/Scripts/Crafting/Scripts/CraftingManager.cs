using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    List<GameObject> horizontalBox;

    private void GetHorizontalChild()
    {
        for (int i = 0; i < horizontalBox.Count; i++)
        {
            for (int j = 0; j < horizontalBox[i].transform.childCount; j++)
            {

            }
        }
    }
}
