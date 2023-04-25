using System;
using UnityEngine;

[RequireComponent(typeof(VelocityController))]
public class MovementController : MonoBehaviour
{
    public static event Action<MovementController> Move;

    private VelocityController cached_VC;
    private VelocityController VC => cached_VC ??= GetComponent<VelocityController>();

    public Vector2 MovementInput { get; set; }
    public float BaseSpeed = 5f;

    void FixedUpdate()
    {
        if (MovementInput != default) Move?.Invoke(this);
        VC.AddAtomicEffect(new(MovementInput.normalized, BaseSpeed, VelocityController.VelocityEffect.BlendingMode.Overwrite, VelocityController.VelocityEffect.ChannelMask.XY), 0);
    }
}
