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

            // wave
            triggeringNPC.GetComponent<Animator>().SetBool("waving", true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                triggeringNPC.GetComponent<Animator>().SetBool("respawn", false);
                triggering = false;
                triggeringNPC.GetComponent<Animator>().SetBool("waving", false);
                triggeringNPC.GetComponent<NpcController>().alive = false;
                triggeringNPC.GetComponent<Animator>().SetBool("Death_01", true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                triggeringNPC.GetComponent<Animator>().SetBool("respawn", false);
                triggering = false;
                triggeringNPC.GetComponent<Animator>().SetBool("waving", false);
                triggeringNPC.GetComponent<NpcController>().alive = false;
                triggeringNPC.GetComponent<Animator>().SetBool("Death_02", true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                triggeringNPC.GetComponent<Animator>().SetBool("respawn", false);
                triggering = false;
                triggeringNPC.GetComponent<Animator>().SetBool("waving", false);
                triggeringNPC.GetComponent<NpcController>().alive = false;
                triggeringNPC.GetComponent<Animator>().SetBool("Death_03", true);
            }
        }

        else
        {
            npcIcon.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E) && triggeringNPC != null && triggeringNPC.GetComponent<NpcController>().alive == false){
                GameObject.Find("Ch03").GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
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
