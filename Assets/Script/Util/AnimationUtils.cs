using System;
using System.Collections;
using UnityEngine;

static class AnimationUtils
{
    public static IEnumerator MoveForSeconds(this VelocityController vc, Vector2 direction, float duration, float speed = 5f)
    {
        vc.AddEffect(new(direction.normalized, speed, VelocityController.VelocityEffect.BlendingMode.Overwrite, VelocityController.VelocityEffect.ChannelMask.XY), duration, 1);
        yield return new WaitForSeconds(duration);
    }

    public static IEnumerator MoveByDampened(this VelocityController vc, Vector2 offset, float duration)
        => vc.MoveDampened(vc.transform.position._xy() + offset, duration);

    public static IEnumerator MoveDampened(this VelocityController vc, Vector2 target, float duration)
    {
        var multiplier = .1f / duration; //FIXME: very rough estimation
        var time = 0f;
        while (time < duration)
        {
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;

            var currentPos = vc.transform.position._xy();
            var step = (target - currentPos) * multiplier;
            
            vc.AddAtomicEffect(new(step.normalized, step.magnitude / Time.fixedDeltaTime, VelocityController.VelocityEffect.BlendingMode.Overwrite, VelocityController.VelocityEffect.ChannelMask.XY), 1);
        }
    }

    public static IEnumerator RunInterpolation(Action<float> action, float duration)
    {
        var time = 0f;
        while (time < duration)
        {
            action(time / duration);
            yield return null;
            time += Time.deltaTime;
        }

        action(1);
    }
}