using System;
using System.Collections.Generic;
using UnityEngine;

public class GrandMA3 : MonoBehaviour
{
    public readonly static List<DiscoLight> lights = new();

    private static LinkedList<ILightingEffect> effectStack = new();
    public static ILightingEffect ActiveEffect => effectStack.First?.Value;

    void Start()
    {
        SetEffect(new RandomLightEffect());
    }

    /// <summary>
    /// Set the currently active effect, possibly replacing another.
    /// </summary>
    /// <returns>A handle that can be used to cancel the effect</returns>
    public static LightingEffectHandle SetEffect(ILightingEffect effect)
    {
        var previous = effectStack.First;

        var node = new LinkedListNode<ILightingEffect>(effect);
        effectStack.AddFirst(node);
        effect.OnEnable();

        // tell the previously active effect that it has been replaced
        if (previous != null)
        {
            var action = previous.Value.OnReplaced();
            if (action == ILightingEffect.ReplaceAction.Cancel)
            {
                effectStack.Remove(previous);
                previous.Value.OnCancelled();
            }
        }

        return new LightingEffectHandle(node);
    }

    void Update()
    {
        ActiveEffect?.UpdateLights(lights);
    }

    void OnDestroy()
    {
        foreach(var effect in effectStack) effect.OnCancelled();
        effectStack.Clear();
    }

    public readonly struct LightingEffectHandle : IDisposable
    {
        private readonly LinkedListNode<ILightingEffect> Node;

        public bool Cancelled => Node?.List == null;
        public bool Active => Node != null && effectStack.First == Node;

        internal LightingEffectHandle(LinkedListNode<ILightingEffect> node)
        {
            Node = node;
        }

        /// <summary>
        /// Cancels the effect
        /// </summary>
        public void Dispose()
        {
            if (Cancelled) return;

            var wasActive = effectStack.First == Node;
            effectStack.Remove(Node);
            ActiveEffect?.OnEnable();
            if (wasActive) Node.Value.OnCancelled();
        }

        /// <summary>
        /// Indicates that this effect will never be cancelled manually and
        /// clears the stack below this effect.
        /// </summary>
        public void IWillNotClose()
        {
            LinkedListNode<ILightingEffect> current;
            while ((current = Node.Next) != null)
            {
                effectStack.Remove(current);
                current.Value.OnCancelled();
            }
        }
    }
}
