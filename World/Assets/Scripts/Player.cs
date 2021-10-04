using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject triggeringNPC;
    public static bool triggering;
    public GameObject npcIcon;

    void Update()
    {

        if (triggering)
        {
            npcIcon.SetActive(true);
            npcIcon.transform.Rotate(Vector3.up, 50f * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                triggering = false;
                triggeringNPC.GetComponent<NpcController>().alive = false;
                triggeringNPC.GetComponent<Animator>().SetBool("Death_01", true);
            }
        }

        else
        {
            npcIcon.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            triggering = true;
            triggeringNPC = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            triggering = false;
            triggeringNPC = null;
        }
    }
}
