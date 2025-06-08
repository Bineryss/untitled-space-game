using UnityEngine;

[CreateAssetMenu(fileName = "Rarity", menuName = "Crafting/Rarity")]
public class Rarity : ScriptableObject
{
    [Header("UI")]
    public Sprite Background;
    public int SortOrder;
    [Header("Gameplay")]
    public int DropChance;
}