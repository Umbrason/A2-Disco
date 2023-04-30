
using System.Collections;
using System.Linq;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class NPCScatter : MonoBehaviour
{
    [SerializeField] private NPC npcTemplate;
    [SerializeField] private Sprite[] sprites;

    float radius = 30f;

    public void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            var npc = Instantiate(npcTemplate);
            npc.GetComponent<SpriteRenderer>().sprite = sprites[i];
            npc.transform.position = Random.insideUnitCircle * radius;
        }
    }



}
