using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject triggeringNPC;
    public static bool triggering;

    private NpcController controllerNPC;
    private Animator animatorNPC;

    void Update()
    {

        if (triggering)
        {            
            // Wave
            triggeringNPC.GetComponent<Animator>().SetBool("waving", true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                deathProtocol("Death_01");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                deathProtocol("Death_02");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                deathProtocol("Death_03");
            }
        }

        else
        {
            // Collect Body
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
            // Stop Wave Animation
            if (triggeringNPC != null)
            {
                triggeringNPC.GetComponent<Animator>().SetBool("waving", false);
            }

            triggering = false;
            triggeringNPC = null;
        }
    }

    private void deathProtocol(string death)
    {
        // Get All Required Components
        animatorNPC   = triggeringNPC.GetComponent<Animator>();
        controllerNPC = triggeringNPC.GetComponent<NpcController>();

        // Stop Wave Animation
        animatorNPC.SetBool("waving", false);

        // Prevent immediate transition to walk animation
        animatorNPC.SetBool("respawn", false);

        // Transition to death animation
        animatorNPC.SetBool(death, true);
        
        triggering = false;
        controllerNPC.alive = false;
    }
}
