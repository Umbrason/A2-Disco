
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class DanceMoveExecutor : MonoBehaviour
{
    private IEnumerator ActiveMove;
    private IDanceMove[] cached_Moves;
    public IDanceMove[] AvailableMoves => cached_Moves ??= GetComponents<IDanceMove>().OrderBy(move => move is ILongDanceMove).ToArray();
    public bool IsDancing => ActiveMove != null;

    private MovementController CachedMovementController;
    private MovementController MovementController => CachedMovementController ??= GetComponent<MovementController>();

    public static event Action<DanceMoveExecutor, IDanceMove> StartDancing;

    public void UseMove(IDanceMove move)
    {
        if (ActiveMove != null) return;
        StartDancing?.Invoke(this, move);
        
        var moveIEnumerator = move.Perform(transform);
        if (move is ILongDanceMove) moveIEnumerator = PerformLongMove(moveIEnumerator);
        moveIEnumerator = WrapMove(moveIEnumerator);

        ActiveMove = moveIEnumerator;
        StartCoroutine(moveIEnumerator);
    }

    private IEnumerator WrapMove(IEnumerator moveEnumerator)
    {
        using var transformReset = new TransformResetter(transform);
        using var moveLock = MovementController.Lock();

        try
        {
            yield return moveEnumerator;
        }
        finally
        {
            ActiveMove = null;
        }
    }

    private IEnumerator PerformLongMove(IEnumerator moveEnumerator)
    {
        using var pauseLightsHandle = GrandMA3.SetEffect(new ILightingEffect.Empty());
        yield return moveEnumerator;
        ActiveMove = null;
    }

    private void OnDisable()
    {
        if (ActiveMove is IDisposable disposable) disposable.Dispose();
    }
}
