using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour 
{
    public static NPCManager instancia;
    private List<GameObject> npc;

    private bool inter = false;

    private GameObject closest;

    public float range = 10f;

    private void Awake()
    {
        instancia = this;
        npc = new List<GameObject>();
    }

    void Start()
    {
        
    }

    void Update()
    {       

            if (Input.GetKeyDown(KeyCode.E) && !inter && npc.Count >= 1)
            {
                float dist = range;
                bool found = false;
                foreach (var item in npc)
                {
                    if ((item.transform.position - transform.position).magnitude < dist)
                    {
                        closest = item;
                        dist = (item.transform.position - transform.position).magnitude;
                        found = true;                
                    }
                    //Debug.Log((item.transform.position - transform.position).magnitude);
                }
                if (found)
                {
                    PlayerControlls.instancia.startInteraction(closest.transform, true);
                    closest.GetComponent<npc>().RotateTwordsPlayer(transform);
                    inter = true;
                    Cursor.visible = true;
                }
            }      
    }

    public void StopInteraction()
    {
        inter = false;
    }

    public void addNewNPC(GameObject obj)
    {
        npc.Add(obj);
    }

    public void removeNpc(GameObject obj)
    {
        if (npc.Contains(obj))        
            npc.Remove(obj);        
    }
}
