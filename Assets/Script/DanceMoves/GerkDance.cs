using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerkDance : MonoBehaviour, IDanceMove
{
    public IEnumerator Perform(Transform target)
    {

        target.localScale += new Vector3(1f, 0f, 0f);
        yield return new WaitForSeconds(.33f);
        target.localScale += new Vector3(0f, 1f, 0f);
        yield return new WaitForSeconds(.33f);
        target.localScale -= new Vector3(0f, 1f, 0f);
        yield return new WaitForSeconds(.33f);
        target.localScale -= new Vector3(1f, 0f, 0f);
    }
}
