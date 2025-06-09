using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Crafting/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<Characteristic> Required;
    public InventoryItem Result;
}