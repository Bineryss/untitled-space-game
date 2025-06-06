using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Game/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] public List<EnemyData> Enemies;

}

[System.Serializable]
public struct EnemyData
{
    [field: SerializeField] public int Ammount { get; private set; }
    [field: SerializeField] public ShipData Ship { get; private set; }
    [field: SerializeField] public WeaponData Weapon { get; private set; }

    public EnemyData(int ammount, ShipData ship, WeaponData weapon)
    {
        Ammount = ammount;
        Ship = ship;
        Weapon = weapon;
    }
}
