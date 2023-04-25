using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementController))]
public class ControlsHandler : MonoBehaviour, Controls.IControlsMapActions
{
    private Controls Input;
    private MovementController cached_mc;
    private MovementController MovementController => cached_mc ??= GetComponent<MovementController>();


    public void Start()
    {
        Input = new();
        Input.ControlsMap.SetCallbacks(this);
        Input.Enable();
    }
    

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementController.MovementInput = context.ReadValue<Vector2>();
    }    
}
