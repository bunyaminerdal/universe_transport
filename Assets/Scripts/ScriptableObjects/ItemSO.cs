using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite uiDisplay;
    public bool isStackable;
    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public ItemSO[] inputs;
}
[System.Serializable]
public class Item
{
    public int slotId;
    public string itemId;
    public string itemName;
    public Sprite uiDisplay;
    public bool isStackable;
    public ItemSO[] inputs;
    public Item(ItemSO item)
    {
        slotId = -1;
        itemId = System.Guid.NewGuid().ToString();
        itemName = item.itemName;
        uiDisplay = item.uiDisplay;
        isStackable = item.isStackable;
        inputs = item.inputs;
    }
}
