
using System.Collections;
using UnityEngine;

public interface IDanceMove
{
    IEnumerator Perform(Transform target);
}
