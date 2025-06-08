using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Game/ShipData")]
public class ShipData : ScriptableObject
{
    [Header("Ship Data")]
    [SerializeField] public GameObject ShipPrefab;
    public Rarity Rarity;

    [Header("Enemy Data")]
    [SerializeField] public float Speed;
    [SerializeField] public IMovementPattern MovementPattern;
    [SerializeField] public int Cost;
    [SerializeField] public float itemDropChance;
}
