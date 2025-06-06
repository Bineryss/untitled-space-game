using UnityEngine.InputSystem;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    public static GameObject SpawnShip(ShipData shipData, WeaponData weaponData, Target target, Transform transform, bool isPlayer = false)
    {
        GameObject ship = Instantiate(shipData.ShipPrefab, transform.position, transform.rotation);
        ConfigureWeapons(ship, weaponData, target, isPlayer);
        ConfigureShipMovement(ship, shipData, isPlayer);

        return ship;
    }

    public static GameObject SpawnShip(ShipData shipData, WeaponData weaponData, Target target, Transform transform, InputActionAsset playerInputActions)
    {
        GameObject ship = SpawnShip(shipData, weaponData, target, transform, true);
        PlayerInput playerInput = ship.AddComponent<PlayerInput>();
        ConfigurePlayerInput(playerInput, playerInputActions);
        PlayerController controller = ship.AddComponent<PlayerController>();
        controller.Instantiate();
        
        return ship;
    }

    private static void ConfigureWeapons(GameObject ship, WeaponData data, Target target, bool isPlayer)
    {
        Target type = isPlayer ? Target.PLAYER : Target.ENEMY;

        ship.tag = type.ToString();
        ship.GetComponent<IDamageable>().TargetType = type;
        ship.GetComponent<WeaponController>().Configure(data, target);
    }

    private static void ConfigureShipMovement(GameObject ship, ShipData data, bool isPlayer)
    {
        if (isPlayer) return;


        IMovementPattern movement = ship.AddComponent<TopEnemyController>();
        movement.StartMovement(data.Speed);
    }
    private static void ConfigurePlayerInput(PlayerInput playerInput, InputActionAsset playerInputActions)
    {
        playerInput.actions = playerInputActions;
        playerInput.defaultActionMap = nameof(InputSystem_Actions.Player); ;
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
    }
}
