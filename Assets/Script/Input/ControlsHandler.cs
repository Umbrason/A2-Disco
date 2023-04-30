using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementController), typeof(DanceMoveExecutor), typeof(LightSpawner)), RequireComponent(typeof(GlobalLightDimmer))]
public class ControlsHandler : MonoBehaviour, Controls.IControlsMapActions
{
    private Controls Input;
    private MovementController cached_mc;
    private MovementController MovementController => cached_mc ??= GetComponent<MovementController>();

    private DanceMoveExecutor CachedDMExecutor;
    private DanceMoveExecutor DanceMoveExecutor => CachedDMExecutor ??= GetComponent<DanceMoveExecutor>();

    private LightSpawner cached_lightSpawner;
    private LightSpawner LightSpawner => cached_lightSpawner ??= GetComponent<LightSpawner>();

    private GlobalLightDimmer cached_GlobalLightDimmer;
    private GlobalLightDimmer GlobalLightDimmer => cached_GlobalLightDimmer ??= GetComponent<GlobalLightDimmer>();

    public void Start()
    {
        Input = new();
        Input.ControlsMap.SetCallbacks(this);
        Input.Enable();
    }

    void OnDestroy()
    {
        Input.Dispose();
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

    public void OnSpawnLight(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        LightSpawner.Spawn();
    }

    public void OnDimLights(InputAction.CallbackContext context)
    {
        float value = -context.ReadValue<float>();
        GlobalLightDimmer.Brightness *= Mathf.Pow(1.1f, value);
    }
}
