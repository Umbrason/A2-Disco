using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonerCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "okspooner";
    [SerializeField] AudioSource spooner;

    public void Execute()
    {
        StartCoroutine(AnnoyTheShitOutOfPeople());
    }

    private IEnumerator AnnoyTheShitOutOfPeople()
    {
        for (int i = 0; i < Random.Range(2, 10); i++)
        {
            spooner.PlayOneShot(spooner.clip, Random.Range(.1f, 1f));
            yield return new WaitForSeconds(Random.Range(.1f, .5f));
        }
    }
}
