using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidGameCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "squidgame";

    private RedGreenLightGame activeEffect = null;

    public void Execute()
    {
        if (activeEffect != null) return;
        activeEffect = new();

        GrandMA3.SetEffect(activeEffect);
        DanceMoveExecutor.StartDancing += OnStartDancing;
        MovementController.Move += OnMove;
        StartCoroutine(ToggleLightColor());
    }

    private void OnDestroy()
    {
        DanceMoveExecutor.StartDancing -= OnStartDancing;
        MovementController.Move -= OnMove;
    }

    private void OnStartDancing(DanceMoveExecutor executor, IDanceMove move)
    {
        if (activeEffect.IsRed) PunishCharacter(executor.gameObject);
    }

    private void OnMove(MovementController movementController)
    {
        if (activeEffect.IsRed) PunishCharacter(movementController.gameObject);
    }

    private void PunishCharacter(GameObject obj) => Destroy(obj, 1);

    private IEnumerator ToggleLightColor()
    {
        while (activeEffect != null)
        {
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            activeEffect.IsRed = !activeEffect.IsRed;
        }
    }

    private class RedGreenLightGame : ILightingEffect
    {
        public bool IsRed = false;

        void ILightingEffect.UpdateLights(List<DiscoLight> lights)
        {
            var color = IsRed ? Color.red : Color.green;
            foreach (var light in lights)
                light.Color = color;
        }
    }
}