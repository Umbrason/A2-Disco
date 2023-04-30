using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatherCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "gather";

    [SerializeField]
    private GameObject player;

    void Start()
    {
        DanceMoveExecutor.StartDancing += OnStartDancing;
    }

    void OnDestroy()
    {
        DanceMoveExecutor.StartDancing -= OnStartDancing;
    }

    void OnStartDancing(DanceMoveExecutor executor, IDanceMove move)
    {
        if (move is ILongDanceMove longMove)
        {
            var duration = longMove.GatherDuration;
            if (duration > 0 && Random.value > .6f)
                Gather(executor.transform.position._xy(), duration);
        }
    }

    public void Execute() => Gather(player.transform.position._xy()); 

    void Gather(Vector2 center, float duration = 2f)
    {
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var targets = rootObjects
            .SelectMany(obj => obj.GetComponentsInChildren<NPC>(false))
            .Where(npc => npc.gameObject.activeInHierarchy)
            .OrderBy(npc => (center - npc.transform.position._xy()).sqrMagnitude)
            .Take(10)
            .OrderBy(_ => Random.value)
            .ToArray();
        if (targets.Length == 0) return;

        var rotationStep = Quaternion.Euler(0, 0, 360f / targets.Length);
        var offset = Vector2.up * 7;

        foreach(var target in targets)
        {
            var targetPos = center + offset;
            target.JoinGathering(targetPos, duration);
            offset = rotationStep * offset;
        }
    }
}
