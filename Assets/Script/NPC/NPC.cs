using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DanceMoveExecutor), typeof(MovementController), typeof(VelocityController))]
[RequireComponent(typeof(BoxCollider2D))]
public class NPC : MonoBehaviour
{
    private DanceMoveExecutor cached_dme;
    private DanceMoveExecutor DanceMoveExecutor => cached_dme ??= GetComponent<DanceMoveExecutor>();

    private MovementController cachedMC;
    private MovementController MC => cachedMC ??= GetComponent<MovementController>();

    private VelocityController cachedVC;
    private VelocityController VC => cachedVC ??= GetComponent<VelocityController>();

    private BoxCollider2D cachedBC;
    private BoxCollider2D BC => cachedBC ??= GetComponent<BoxCollider2D>();

    public float minDanceDelay, maxDanceDelay;
    private float nextMoveTime;

    private Vector2 home;
    private float noiseSeed = 0;

    private Coroutine currentGathering;

    void Start()
    {
        home = transform.position._xy();
        noiseSeed = Random.value;
    }

    void FixedUpdate()
    {
        // don't wander home during gatherings
        if (currentGathering != null)
        {
            MC.MovementInput = Vector2.zero;
            return;
        }

        var movement = home - transform.position._xy();
        if (movement.sqrMagnitude < .1f)
        {
            MC.MovementInput = Vector2.zero;
            return;
        }

        var wanderingFactor = Mathf.Min(movement.sqrMagnitude, 3f) / 3f;
        var wanderingRotation = Quaternion.Euler(0, 0, 120 * (Mathf.PerlinNoise(Time.time, noiseSeed) - .5f) * wanderingFactor);
        MC.MovementInput = wanderingRotation * movement;
        MC.BaseSpeed = 5 * Mathf.PerlinNoise(Time.time, noiseSeed + .4f);
    }

    public void JoinGathering(Vector2 target, float duration)
    {
        if (currentGathering != null) StopCoroutine(currentGathering);
        currentGathering = StartCoroutine(Gathering(target, duration));
    }

    private IEnumerator Gathering(Vector2 target, float duration)
    {
        BC.enabled = false;
        yield return VC.MoveDampened(target, .5f);
        BC.enabled = true;
        yield return new WaitForSeconds(duration);
        currentGathering = null;
    }

    void Update()
    {
        // don't dance during gatherings
        if (currentGathering != null) return;

        if (nextMoveTime > Time.time) return;
        nextMoveTime = Time.time + Random.Range(minDanceDelay, maxDanceDelay);
        if (DanceMoveExecutor.IsDancing) return;
        var danceMoveIndex = Random.Range(0, DanceMoveExecutor.AvailableMoves.Length);
        DanceMoveExecutor.UseMove(DanceMoveExecutor.AvailableMoves[danceMoveIndex]);
    }
}
