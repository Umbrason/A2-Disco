using UnityEngine;

[RequireComponent(typeof(DanceMoveExecutor))]
public class NPC : MonoBehaviour
{
    private DanceMoveExecutor cached_dme;
    private DanceMoveExecutor DanceMoveExecutor => cached_dme ??= GetComponent<DanceMoveExecutor>();

    public float minDanceFrequency, maxDanceFrequency;
    private float nextMoveTime;

    void Update()
    {
        
        if (nextMoveTime > Time.time) return;
        nextMoveTime = Time.time + Random.Range(minDanceFrequency, maxDanceFrequency);
        if (DanceMoveExecutor.IsDancing) return;
        var danceMoveIndex = Random.Range(0, DanceMoveExecutor.AvailableMoves.Length);
        DanceMoveExecutor.UseMove(DanceMoveExecutor.AvailableMoves[danceMoveIndex]);
    }
}
