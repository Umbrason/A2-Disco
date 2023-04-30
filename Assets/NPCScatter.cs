
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCScatter : MonoBehaviour
{
    [SerializeField] private NPC npcTemplate;
    [SerializeField] private Sprite[] sprites;

    float radius = 45f / 2f;

    private readonly List<Vector2> positions = new();

    public void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            var npc = Instantiate(npcTemplate);
            npc.GetComponent<SpriteRenderer>().sprite = sprites[i];
            while (true)
            {
                var pos = Random.insideUnitCircle * radius;
                if (positions.Any(p => Vector2.Distance(pos, p) < 3f))
                    continue;
                positions.Add(pos);
                npc.transform.position = pos;
                break;
            }

        }
    }



}
