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
        SpawnPlayerShip();
    }

    private GameObject SpawnPlayerShip()
    {
        GameObject playerShip = Instantiate(ship.ShipPrefab);
        playerShip.GetComponent<WeaponController>().Configure(data, Target.ENEMY);
        PlayerInput playerInput = playerShip.AddComponent<PlayerInput>();
        ConfigurePlayerInput(playerInput);
        PlayerController controller = playerShip.AddComponent<PlayerController>();
        controller.Instantiate();

        ConfigureAsPlayerShip(playerShip);
        return playerShip;
    }

    private void ConfigurePlayerInput(PlayerInput playerInput)
    {
        playerInput.actions = playerInputActions;
        playerInput.defaultActionMap = nameof(InputSystem_Actions.Player);;
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
    }

    private void ConfigureAsPlayerShip(GameObject playerShip)
    {
        playerShip.tag = Target.PLAYER.ToString();
    }

}
