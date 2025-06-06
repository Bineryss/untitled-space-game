using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 15.0f;

    private Vector2 moveInput;
    private bool disabled;
    private InputSystem_Actions actions;


    public void Instantiate()
    {
        // Instantiate and hook up the generated actions
        actions = new InputSystem_Actions();
        actions.Player.SetCallbacks(this);
        actions.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
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
