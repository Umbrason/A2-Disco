using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportsBehindEveryone : MonoBehaviour, ILongDanceMove
{
    public GameObject vfx;
    public GameObject strikeVfx;

    public IEnumerator Perform(Transform target)
    {
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var NPCs = rootObjects.SelectMany(obj => obj.GetComponentsInChildren<NPC>(false)).Where(NPC => NPC.gameObject.activeInHierarchy && NPC.transform != target).ToArray();
        if (NPCs.Length == 0) yield break;
        var originalPosition = target.transform.position;

        var lastPos = target.transform.position;
        var npcList = new List<NPC>(NPCs);
        var killQueue = new Queue<NPC>(NPCs);
        while (npcList.Count > 0)
        {
            var nearest = npcList.OrderBy(npc => (npc.transform.position - lastPos)._xy().magnitude).First();
            npcList.Remove(nearest);
            lastPos = nearest.transform.position;
            killQueue.Enqueue(nearest);
        }

        while (killQueue.Count > 0)
        {
            var victim = killQueue.Dequeue();
            if (victim == null) continue;
            target.transform.position = victim.transform.position._xy() + Vector2.up * 1f;
            yield return new WaitForSeconds(.15f);
            Instantiate(strikeVfx, victim.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.05f);
        }
        if (target != null) target.transform.position = originalPosition;
        yield return new WaitForSeconds(.7f);
        foreach (var npc in NPCs)
        {
            if (npc == null) continue;
            npc.gameObject.SetActive(false);
            Instantiate(vfx, npc.transform.position, Quaternion.identity);
        }
        StartCoroutine(ReviveAll(NPCs));
    }

    IEnumerator ReviveAll(NPC[] NPCs)
    {
        yield return new WaitForSeconds(4.5f);
        foreach (var npc in NPCs)
        {
            if (npc == null) continue;
            npc.gameObject.SetActive(true);
        }
    }
}