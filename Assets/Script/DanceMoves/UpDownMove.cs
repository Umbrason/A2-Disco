using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VelocityController))]
public class UpDownMove : MonoBehaviour, IDanceMove
{
    public IEnumerator Perform(Transform target)
    {
        var vc = GetComponent<VelocityController>();
        yield return vc.MoveForSeconds(Vector2.up, .3f, 10);
        yield return new WaitForSeconds(.3f);
        yield return vc.MoveForSeconds(Vector2.down, .3f, 10);
    }
}
