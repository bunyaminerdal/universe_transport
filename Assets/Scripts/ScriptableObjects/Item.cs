using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    public Sprite uiDisplay;
    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public Item[] inputs;
}
