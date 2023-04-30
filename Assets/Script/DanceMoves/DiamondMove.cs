using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VelocityController))]
public class DiamondMove : MonoBehaviour, IDanceMove
{
    public IEnumerator Perform(Transform target)
    {
        var vc = GetComponent<VelocityController>();
        yield return vc.MoveForSeconds(Vector2.down + Vector2.right, .25f, 10);
        yield return new WaitForSeconds(.25f);
        yield return vc.MoveForSeconds(Vector2.down + Vector2.left, .25f, 10);
        yield return new WaitForSeconds(.25f);
        yield return vc.MoveForSeconds(Vector2.up + Vector2.left, .25f, 10);
        yield return new WaitForSeconds(.25f);
        yield return vc.MoveForSeconds(Vector2.up + Vector2.right, .25f, 10);
        yield return new WaitForSeconds(.25f);
    }
}
