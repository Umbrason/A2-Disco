
using System.Collections;
using UnityEngine;

public class DanceMoveExecutor : MonoBehaviour
{
    private Coroutine ActiveMove;
    private IDanceMove[] cached_Moves;
    public IDanceMove[] AvailableMoves => cached_Moves ??= GetComponents<IDanceMove>();

    private void UseMove(IDanceMove move)
    {
        if (ActiveMove != null) return;
        var MoveIEnumerator = move is ILongDanceMove ? PerformLongMove(move.Perform(transform)) : move.Perform(transform);
        ActiveMove = StartCoroutine(move.Perform(transform));
    }

    private IEnumerator PerformLongMove(IEnumerator moveEnumerator)
    {
        //toggle lights
        yield return moveEnumerator;
        //toggle lights
    }

}
