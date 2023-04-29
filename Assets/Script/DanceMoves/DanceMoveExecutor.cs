
using System;
using System.Collections;
using UnityEngine;

public class DanceMoveExecutor : MonoBehaviour
{
    private Coroutine ActiveMove;
    private IDanceMove[] cached_Moves;
    public IDanceMove[] AvailableMoves => cached_Moves ??= GetComponents<IDanceMove>();
    public bool IsDancing => ActiveMove != null;

    public static event Action<DanceMoveExecutor, IDanceMove> StartDancing;

    public void UseMove(IDanceMove move)
    {
        if (ActiveMove != null) return;
        StartDancing?.Invoke(this, move);
        var moveIEnumerator = move is ILongDanceMove ? PerformLongMove(move.Perform(transform)) : move.Perform(transform);
        ActiveMove = StartCoroutine(moveIEnumerator);
    }

    private IEnumerator PerformLongMove(IEnumerator moveEnumerator)
    {
        using var pauseLightsHandle = GrandMA3.SetEffect(ILightingEffect.Empty.KeptWhenReplaced());
        yield return moveEnumerator;
    }

}
