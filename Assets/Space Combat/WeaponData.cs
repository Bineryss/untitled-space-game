using Core;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/WeaponData")]
public class WeaponData : InventoryItem
{
    [SerializeField] public GameObject ProjectilePrefab;
    [SerializeField] public int Speed = 1;
    [SerializeField] public float FireRate = 1.0f;
    [SerializeField] public int Damage = 1;
    [SerializeField] public int ActiveTime = 100;
    [SerializeField] public int SalvoAmmount = 1;
    [SerializeField] public float SalvoDelay = 1.0f;

    [Header("Enemey Modifier")]
    [SerializeField] public float SpeedDelay = 0.5f;
    [SerializeField] public float FireRateDelay = 0.5f;

    public override void ChangeQuantity(InventoryManager manager, int quantity)
    {
        manager.ChangeQuantity(this, quantity);
    }

    public override void HasEnough(InventoryManager manager, int quantity)
    {
        manager.HasEnough(this, quantity);
    }
}
