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