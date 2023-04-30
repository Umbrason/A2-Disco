using System.Collections;
using UnityEngine;

public class ScaleMove : MonoBehaviour, IDanceMove
{
    public IEnumerator Perform(Transform target)
    {
        var originalScale = transform.localScale;
        yield return AnimationUtils.RunInterpolation((t) =>
        {
            transform.localScale = originalScale * (1 + Mathf.Sin(Mathf.PI * t));
        }, .5f);
        transform.localScale = originalScale;
    }
}
