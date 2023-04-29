
using System.Collections.Generic;

public interface ILightingEffect
{
    public void UpdateLights(IEnumerable<DiscoLight> lights);

    /// <summary>
    /// Called when the effect has been cancelled.
    /// </summary>
    public void OnCancelled() { }

    /// <summary>
    /// Called when the effect has been replaced.
    /// If not overridden, the default implementation cancels the effect (<see cref="ReplaceAction.Cancel"/>).
    /// </summary>
    /// <returns>What the light controller shoud do with this effect</returns>
    public ReplaceAction OnReplaced() => ReplaceAction.Cancel;

    /// <summary>
    /// Called when the effect is enabled for the first time or after it was previously replaced
    /// </summary>
    public void OnEnable() { }

    public enum ReplaceAction
    {
        /// <summary>
        /// Cancel the effect. It won't be enabled again in the future.
        /// </summary>
        Cancel,

        /// <summary>
        /// Keep the effect in the effect stack. It will be enabled again after any replacing effects have been cancelled.
        /// </summary>
        Keep,
    }

    /// <summary>
    /// The empty effect: does not update lights
    /// </summary>     
    public class Empty : ILightingEffect
    {
        private readonly ReplaceAction ReplaceAction;

        Empty(ReplaceAction replaceAction = ReplaceAction.Cancel)
        {
            ReplaceAction = replaceAction;
        }

        public static Empty KeptWhenReplaced() => new Empty(ReplaceAction.Keep);

        void ILightingEffect.UpdateLights(IEnumerable<DiscoLight> lights) { }

        ReplaceAction ILightingEffect.OnReplaced() => ReplaceAction;
    }
}
