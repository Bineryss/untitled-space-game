using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    [Header("Inventory")]
    public string Name;
    [TextArea]
    public string Description;
    public Sprite Icon;
    public Rarity Rarity;
}