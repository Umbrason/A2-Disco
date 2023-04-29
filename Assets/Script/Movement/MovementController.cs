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

    private uint lockCount = 0;
    public bool Locked => lockCount > 0;

    void FixedUpdate()
    {
        var input = lockCount > 0 ? Vector2.zero : MovementInput;
        if (input != default) Move?.Invoke(this);
        VC.AddAtomicEffect(new(input.normalized, BaseSpeed, VelocityController.VelocityEffect.BlendingMode.Overwrite, VelocityController.VelocityEffect.ChannelMask.XY), 0);
    }

    public IDisposable Lock() => new MovementLock(this);

    private class MovementLock : IDisposable
    {
        private MovementController Controller;

        public MovementLock(MovementController controller)
        {
            Controller = controller;
            controller.lockCount++;
        }

        public void Dispose()
        {
            if (Controller == null) return;
            Controller.lockCount--;
            Controller = null;
        }
    }
}
