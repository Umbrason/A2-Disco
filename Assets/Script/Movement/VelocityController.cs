using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityController : MonoBehaviour
{
    private Rigidbody2D cached_rb;
    private Rigidbody2D RB => cached_rb ??= GetComponent<Rigidbody2D>();

    private readonly Dictionary<Guid, VelocityEffectInstance> ActiveEffects = new();

    public Guid AddAtomicEffect(VelocityEffect effect, int effectLayer) => AddEffect(effect, Time.fixedDeltaTime, effectLayer);
    public Guid AddEffect(VelocityEffect effect, float duration, int effectLayer)
    {
        var instance = new VelocityEffectInstance(effect, duration, effectLayer);
        ActiveEffects.Add(instance.Guid, instance);
        return instance.Guid;
    }

    public Guid AddAtomicSlowdownEffect(float amount, int effectLayer, VelocityEffect.ChannelMask mask = VelocityEffect.ChannelMask.XY)
        => AddEffect(new VelocityEffect(Vector2.one, amount, VelocityEffect.BlendingMode.Multiply, mask), Time.fixedDeltaTime, effectLayer);

    public Guid AddSlowdownEffect(float amount, float duration, int effectLayer, VelocityEffect.ChannelMask mask = VelocityEffect.ChannelMask.XY)
        => AddEffect(new VelocityEffect(Vector2.one, amount, VelocityEffect.BlendingMode.Multiply, mask), duration, effectLayer);
    public void ClearEffect(Guid guid) => ActiveEffects.Remove(guid);
    public void ModifyEffect(Guid guid, VelocityEffect effect) => ActiveEffects[guid].Effect = effect;


    void FixedUpdate()
    {
        var velocity = RB.velocity;
        var sortedEffects = ActiveEffects.Values.OrderBy(i => i.EffectLayer).ToList();
        foreach (var effectInstance in sortedEffects)
        {
            var maskedVelocity = Vector2.Scale(effectInstance.Velocity, effectInstance.MaskVector);
            var inverseMaskVector = Vector2.one - effectInstance.MaskVector;
            switch (effectInstance.Effect.Blending)
            {
                case VelocityEffect.BlendingMode.Overwrite:
                    velocity = Vector2.Scale(velocity, inverseMaskVector) + maskedVelocity;
                    break;
                case VelocityEffect.BlendingMode.Multiply:
                    velocity = Vector2.Scale(velocity, maskedVelocity + inverseMaskVector);
                    break;
                case VelocityEffect.BlendingMode.Additive:
                    velocity = maskedVelocity + velocity;
                    break;
            }

            //check if effect will run out before next fixedUpdate
            var time = Time.fixedTime;
            if (Time.fixedTime + Time.fixedDeltaTime * 1.5f >= effectInstance.StartTime + effectInstance.Duration)
            {
                ActiveEffects.Remove(effectInstance.Guid);
                effectInstance.Effect.InvokeExpired();
            }
        }
        RB.AddForce((velocity - RB.velocity) * RB.mass, ForceMode2D.Impulse);
    }

    private class VelocityEffectInstance
    {
        public Guid Guid { get; set; }
        public float StartTime { get; set; }
        public float Duration { get; set; }
        private int baseEffectLayer; //applied from lowest to highest. Higher layers can overwrite lower ones
        public VelocityEffect Effect { get; set; }

        public int EffectLayer => baseEffectLayer * 10 + (int)Effect.Blending;
        public Vector2 Velocity => Effect.Direction * Effect.Speed(Mathf.Clamp01((Time.time - StartTime) / Duration));
        public Vector2 MaskVector => new Vector2(((int)Effect.Mask >> 1) & 1, ((int)Effect.Mask) & 1);

        public VelocityEffectInstance(VelocityEffect effect, float duration, int effectLayer)
        {
            this.Guid = Guid.NewGuid();
            this.StartTime = Time.fixedTime;
            this.Duration = duration;
            this.baseEffectLayer = effectLayer;
            this.Effect = effect;
        }
    }

    public struct VelocityEffect
    {
        public enum ChannelMask
        {
            X = 0b10,
            Y = 0b01,
            XY = 0b11,
        }
        public enum BlendingMode
        {
            Overwrite = 0,
            Additive = 1,
            Multiply = 2
        }
        public ChannelMask Mask { get; set; }
        public BlendingMode Blending { get; set; }
        public Vector2 Direction { get; set; }
        public Func<float, float> Speed { get; set; }

        public event Action OnExpired;
        internal void InvokeExpired() => OnExpired?.Invoke();

        public VelocityEffect(Vector2 direction, float constantSpeed, BlendingMode blending, ChannelMask mask = ChannelMask.XY, Action onExpired = null) : this(direction, _ => constantSpeed, blending, mask, onExpired) { }
        public VelocityEffect(Vector2 direction, AnimationCurve speedCurve, BlendingMode blending, ChannelMask mask = ChannelMask.XY, Action onExpired = null) : this(direction, speedCurve.Evaluate, blending, mask, onExpired) { }
        public VelocityEffect(Vector2 direction, Func<float, float> speedFunction, BlendingMode blending, ChannelMask mask = ChannelMask.XY, Action onExpired = null)
        {
            this.Direction = direction;
            this.Speed = speedFunction;
            this.Blending = blending;
            this.Mask = mask;
            this.OnExpired = onExpired;
        }

    }
}
