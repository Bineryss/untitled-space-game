using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Game/ShipData")]
public class ShipData : InventoryItem
{
    [Header("Ship Data")]
    [SerializeField] public GameObject ShipPrefab;

    [Header("Enemy Data")]
    [SerializeField] public float Speed;
    [SerializeField] public IMovementPattern MovementPattern;
    [SerializeField] public int Cost;
    [SerializeField] public float itemDropChance;
}
