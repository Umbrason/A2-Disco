using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(VelocityController))]
public class NinjaMode : MonoBehaviour
{
    private SpriteRenderer cachedRenderer = null;
    private SpriteRenderer Renderer => cachedRenderer ??= GetComponent<SpriteRenderer>();

    private VelocityController cachedVelocityController = null;
    private VelocityController VelocityController => cachedVelocityController ??= GetComponent<VelocityController>();

    private bool active = false;
    private Guid slowdownEffect = Guid.Empty;

    public void Toggle()
    {
        active = !active;

        var color = Renderer.color;
        color.a = active ? 0.5f : 1f;
        Renderer.color = color;

        if (slowdownEffect != Guid.Empty)
        {
            VelocityController.ClearEffect(slowdownEffect);
            slowdownEffect = Guid.Empty;
        }

        if (active)
            slowdownEffect = VelocityController.AddSlowdownEffect(0.5f, float.PositiveInfinity, 10);
    }
}
