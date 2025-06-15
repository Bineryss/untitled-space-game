using UnityEngine;
using Core;

public abstract class InventoryItem : ScriptableObject
{
    public string Id = System.Guid.NewGuid().ToString();

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Generate a new ID only if missing
        if (string.IsNullOrEmpty(Id))
        {
            Id = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif

    [Header("Inventory")]
    public string Name;
    [TextArea]
    public string Description;
    public Sprite Icon;
    public Rarity Rarity;

    public abstract void ChangeQuantity(InventoryManager manager, int quantity);
    public abstract void HasEnough(InventoryManager manager, int quantity);
}