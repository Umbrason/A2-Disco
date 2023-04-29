using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerkLongDance : MonoBehaviour, ILongDanceMove
{
    public IEnumerator Perform (Transform target)
    {
        for (int repeat = 0; repeat < 6; repeat++)
        {
            for (int i = 0; i < 11; i++)
            {
                target.position += Vector3.up * (i / 10);
                target.Rotate((new Vector3(0, 0, .1f) * i));
            }
            for (int i = 0; i < 11; i++)
            {
                target.position += Vector3.right * (i / 10);
                target.Rotate(new Vector3(0, 0, .1f) * i);
            }
            for (int i = 0; i < 11; i++)
            {
                target.position += Vector3.down * (i / 10);
                target.Rotate(new Vector3(0, 0, .1f) * i);
            }
            for (int i = 0; i < 11; i++)
            {
                target.position += Vector3.left * (i / 10);
                target.Rotate(new Vector3(0, 0, .1f) * i);
            }
            for (int i = 0; i < 11; i++)
            {
                target.localScale += new Vector3(.1f, .1f, 0) * i;
            }
            for (int i = 0; i < 11; i++)
            {
                target.localScale -= new Vector3(.1f, .1f, 0) * i;
            }

            if (repeat % 2 == 0)
            {
                target.localScale += new Vector3(1f, 0, 0);
                target.localScale += new Vector3(0, -1f, 0);
            }
            else
            {
                target.localScale += new Vector3(-1f, 0, 0);
                target.localScale += new Vector3(0, 1f, 0);
            }
        }

        yield return null;
    }
}
