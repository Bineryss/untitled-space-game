using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Rarity", menuName = "Crafting/Rarity")]
public class Rarity : ScriptableObject, IComparable
{
    [Header("UI")]
    public Sprite Background;
    public int SortOrder;
    [Header("Gameplay")]
    public int DropChance;

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        Rarity compareObject = obj as Rarity;
        if (compareObject == null) return 1;

        return SortOrder - compareObject.SortOrder;
    }
}