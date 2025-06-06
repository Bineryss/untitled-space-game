using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Game/ShipData")]
public class ShipData : ScriptableObject
{
    [SerializeField] private GameObject shipPrefab;

    public GameObject ShipPrefab => shipPrefab;
}
