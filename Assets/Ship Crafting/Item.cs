using Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Crafting/Item")]
public class Item : InventoryItem
{
    [Header("Gameplay")]
    public Characteristic CraftItem = Characteristic.NOTHING;

    public override void ChangeQuantity(InventoryManager manager, int quantity)
    {
        manager.ChangeQuantity(this, quantity);
    }

    public override void HasEnough(InventoryManager manager, int quantity)
    {
        manager.HasEnough(this, quantity);
    }
}

[System.Serializable]
public struct ResourceQuantity
{
    public Item Ressorce;
    public int Quantity;
}