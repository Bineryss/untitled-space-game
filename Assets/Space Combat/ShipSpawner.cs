using UnityEngine;
using UnityEngine.InputSystem;

public class ShipSpawner : MonoBehaviour
{
    [Header("Player Configuration")]
    [SerializeField] private WeaponData data;
    [SerializeField] private ShipData ship;
    [SerializeField] private InputActionAsset playerInputActions;


    void Start()
    {
        ShipFactory.SpawnShip(ship, data, Target.ENEMY, gameObject.transform, playerInputActions);
    }
}
