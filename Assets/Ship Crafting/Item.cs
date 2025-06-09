using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Crafting/Item")]
public class Item : InventoryItem
{
    [Header("Gameplay")]
    public Characteristic CraftItem = Characteristic.NOTHING;
}

[System.Serializable]
public struct ResourceQuantity
{
    public Item Ressorce;
    public int Quantity;
}