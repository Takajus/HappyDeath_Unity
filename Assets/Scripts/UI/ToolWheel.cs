using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolWheel : MonoBehaviour
{
    public Item item;
    /*private ShortcutWheel shortcutWheel;

    private void Start()
    {
        shortcutWheel =  GameObject.Find("shortcutWheel").GetComponent<ShortcutWheel>();
        shortcutWheel.onToolCreated += AssignToolSelected;
        shortcutWheel.onToolCreated += AssignToolSelected;
    }

    private void AssignToolSelected()
    {
        shortcutWheel.toolSelected = this.gameObject;
    }

    private void DeselectTool()
    {
        shortcutWheel.toolSelected = null;
    }*/
}
