using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementController), typeof(DanceMoveExecutor))]
public class ControlsHandler : MonoBehaviour, Controls.IControlsMapActions
{
    private Controls Input;
    private MovementController cached_mc;
    private MovementController MovementController => cached_mc ??= GetComponent<MovementController>();

    private DanceMoveExecutor CachedDMExecutor;
    private DanceMoveExecutor DanceMoveExecutor => CachedDMExecutor ??= GetComponent<DanceMoveExecutor>();

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

    public void OnSelectDanceMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        var index = (int)(context.ReadValue<float>()) - 1;

        var moves = DanceMoveExecutor.AvailableMoves;
        if (index < 0 || index >= moves.Length) return;
        DanceMoveExecutor.UseMove(moves[index]);
    }
}
