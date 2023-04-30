using System.Collections;
using UnityEngine;

public class JartregFancyMove : MonoBehaviour, IDanceMove, ILongDanceMove
{
    public float GatherDuration => 7f;

    public IEnumerator Perform(Transform target)
    {
        var originalScale = transform.localScale;
        var vc = GetComponent<VelocityController>();

        yield return vc.MoveByDampened(Vector2.right * 2, .5f);
        yield return vc.MoveByDampened(Vector2.left * 2, .5f);

        StartCoroutine(ScaleAnimation(originalScale));
        yield return vc.MoveByDampened(Vector2.right * 4, .5f);
        yield return vc.MoveByDampened(Vector2.left * 4, .5f);

        StartCoroutine(ScaleAnimation(originalScale, 3));
        yield return vc.MoveByDampened(new Vector2(-3, 3), .25f);
        yield return vc.MoveByDampened(new Vector2(-3, -3), .25f);
        yield return vc.MoveByDampened(new Vector2(3, -3), .25f);
        yield return vc.MoveByDampened(new Vector2(3, 3), .25f);

        StartCoroutine(ScaleAnimation(originalScale, 3));
        yield return vc.MoveByDampened(new Vector2(3, 3), .25f);
        yield return vc.MoveByDampened(new Vector2(3, -3), .25f);
        yield return vc.MoveByDampened(new Vector2(-3, -3), .25f);
        yield return vc.MoveByDampened(new Vector2(-3, 3), .25f);

        yield return AnimationUtils.RunInterpolation((t) =>
        {
            transform.localScale = originalScale * (1 + 20 * t);
        }, .125f);
        yield return AnimationUtils.RunInterpolation((t) =>
        {
            transform.localScale = originalScale * (1 + 20 * EaseIOExpo(1 - t));
        }, 2);
        transform.localScale = originalScale;
    }

    private IEnumerator ScaleAnimation(Vector3 origin, float scaler = 2)
    {
        var scaled = origin * scaler;
        yield return AnimationUtils.RunInterpolation((t) =>
        {
            transform.localScale = Vector3.Lerp(origin, scaled, Mathf.Sin(Mathf.PI * t));
        }, .5f);
        yield return AnimationUtils.RunInterpolation((t) =>
        {
            transform.localScale = Vector3.Lerp(origin, scaled, Mathf.Sin(Mathf.PI * t));
        }, .5f);
    }

    private float EaseIOExpo(float x) => x < 0.5
        ? Mathf.Pow(2, 20 * x - 10) / 2
        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
}
