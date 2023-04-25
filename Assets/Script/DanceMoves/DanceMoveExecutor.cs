
using System.Collections;
using UnityEngine;

public class DanceMoveExecutor : MonoBehaviour
{
    private Coroutine ActiveMove;
    private IDanceMove[] cached_Moves;
    public IDanceMove[] AvailableMoves => cached_Moves ??= GetComponents<IDanceMove>();
    public bool IsDancing => ActiveMove != null;

    private void UseMove(IDanceMove move)
    {
        if (ActiveMove != null) return;
        var moveIEnumerator = move is ILongDanceMove ? PerformLongMove(move.Perform(transform)) : move.Perform(transform);
        ActiveMove = StartCoroutine(moveIEnumerator);
    }

    private IEnumerator PerformLongMove(IEnumerator moveEnumerator)
    {
        //toggle lights
        yield return moveEnumerator;
        //toggle lights
    }

}
