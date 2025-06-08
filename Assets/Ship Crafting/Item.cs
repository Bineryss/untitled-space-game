using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Crafting/Item")]
public class Item : ScriptableObject
{
    [Header("UI")]
    public string Name = "<Item Name>";
    public string Description = "<Item Desription>";
    public Sprite Sprite;

    [Header("Gameplay")]
    public Characteristic CraftItem = Characteristic.NOTHING;
    public Rarity Rarity;
}

[System.Serializable]
public struct ResourceQuantity
{
    public Item Ressorce;
    public int Quantity;
}

public interface ICraftable
{
    public Rarity Rarity { get; set; }
}

public class CraftingManager
{
    //Some data sctructure that has as key materials and as value the craftable part
    //Map<List<Item>,ICraftable>
    //this way i can map multiple item combination to the same ship or weapon type
    //what happens if i add the same reciepe twice?
}