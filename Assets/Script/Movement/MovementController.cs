using UnityEngine;

[RequireComponent(typeof(VelocityController))]
public class MovementController : MonoBehaviour
{
    private VelocityController cached_VC;
    private VelocityController VC => cached_VC ??= GetComponent<VelocityController>();

    public Vector2 MovementInput { get; set; }
    public float BaseSpeed = 5f;

    void FixedUpdate()
    {
        VC.AddAtomicEffect(new(MovementInput.normalized, BaseSpeed, VelocityController.VelocityEffect.BlendingMode.Overwrite, VelocityController.VelocityEffect.ChannelMask.XY), 0);
    }
}
