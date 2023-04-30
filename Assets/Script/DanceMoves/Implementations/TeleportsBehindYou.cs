using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportsBehindYou : MonoBehaviour, IDanceMove
{
    public GameObject bloodVfx;
    public GameObject strikeVfx;

    public IEnumerator Perform(Transform target)
    {
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var NPCs = rootObjects.SelectMany(obj => obj.GetComponentsInChildren<NPC>()).Where(NPC => NPC.gameObject.activeInHierarchy && NPC.transform != target).ToArray();
        if (NPCs.Length == 0) yield break;
        var originalPosition = target.transform.position;
        var victim = NPCs[Random.Range(0, NPCs.Length)];
        target.transform.position = victim.transform.position._xy() + Vector2.up * 1f;
        yield return new WaitForSeconds(.15f);
        if (victim == null) yield break;
        Instantiate(strikeVfx, victim.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.7f);
        if (victim == null) yield break;
        victim.gameObject.SetActive(false);
        Instantiate(bloodVfx, victim.transform.position, Quaternion.identity);
        if (target == null) yield break;
        target.transform.position = originalPosition;
        StartCoroutine(Revive(victim));
    }

    IEnumerator Revive(NPC victim)
    {
        yield return new WaitForSeconds(4.5f);
        if (victim == null) yield break;
        victim.gameObject.SetActive(true);
    }


}