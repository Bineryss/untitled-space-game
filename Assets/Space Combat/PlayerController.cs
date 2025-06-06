using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5.0f;

    private Vector2 moveInput;
    private bool disabled;


    void Start()
    {
        // Debug the PlayerInput setup
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            Debug.Log($"✅ PlayerInput found");
            Debug.Log($"Actions Asset: {playerInput.actions?.name}");
            Debug.Log($"Current Action Map: {playerInput.currentActionMap?.name}");
            Debug.Log($"Notification Behavior: {playerInput.notificationBehavior}");
        }
        else
        {
            Debug.LogError("❌ PlayerInput component missing!");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"🎮 OnMove triggered: {context.ReadValue<Vector2>()}");
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (disabled) return;

        Vector2 position = (Vector2)transform.position + speed * Time.deltaTime * moveInput;
        transform.position = position;
    }

    public void DisableMovement() => disabled = true;
    public void EnableMovement() => disabled = false;

}
