using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static IDictionary<string, GameObject> triggeringNPC = new Dictionary<string, GameObject>();
    public static bool triggering;

    public static int score = 0;
    public static int starpoint = 0;
    public static int starpoint_hard = 0;
    public static string objective;
    private List<string> Objectives;
    
    private NpcController controllerNPC;
    private Animator animatorNPC;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private HealthBar healthBar;

    void Start()
    {
        score = 0;
        starpoint = 0;
        starpoint_hard = 0;
        Time.timeScale = 1f;
        Objectives = new List<string> { "Businessman", "Doctor", "Child", "Paladin", "SWAT Officer" };
        objective = Objectives[Random.Range(0, Objectives.Count)];

        GetComponent<Health>().HealthChanged += (oldHealth, newHealth) =>
        {
            healthBar.SetDisplayValue(newHealth);
            
            if (newHealth == 0)
            {
                //TODO DEATH
                Time.timeScale = 0f;
                GameUI.SetActive(false);
                GameOverUI.SetActive(true);
            }
        };
    }

    void Update()
    {
        if (triggeringNPC != null)
        {
            foreach (KeyValuePair<string, GameObject> entry in triggeringNPC)
            {
                // do something with entry.Value or entry.Key

                if (triggering)
                {
                    // Wave
                    entry.Value.GetComponent<Animator>().SetBool("waving", true);

                    if (Input.GetKeyDown(KeyCode.Alpha1) && GetComponent<Rigidbody>().velocity.magnitude < 5.00)
                    {
                        deathProtocol("Death_01", entry.Key);
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha2) && GetComponent<Rigidbody>().velocity.magnitude < 5.00)
                    {
                        deathProtocol("Death_02", entry.Key);
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha3) && GetComponent<Rigidbody>().velocity.magnitude < 5.00)
                    {
                        deathProtocol("Death_03", entry.Key);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            triggering = true;

            // if NPC not in list yet, add it
            if (!triggeringNPC.ContainsKey(collider.gameObject.name))
            {
                triggeringNPC.Add(collider.gameObject.name, collider.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            // Stop Wave Animation
            if (triggeringNPC.ContainsKey(collider.gameObject.name))
            {
                triggeringNPC[collider.gameObject.name].GetComponent<Animator>().SetBool("waving", false);
            }

            triggering = false;
            triggeringNPC.Remove(collider.gameObject.name);
        }
    }

    private void deathProtocol(string death, string NPC)
    {
        // Get All Required Components
        animatorNPC = triggeringNPC[NPC].GetComponent<Animator>();
        controllerNPC = triggeringNPC[NPC].GetComponent<NpcController>();

        // Stop Wave Animation
        animatorNPC.SetBool("waving", false);

        // Prevent immediate transition to walk animation
        animatorNPC.SetBool("respawn", false);

        // Transition to death animation
        animatorNPC.SetBool(death, true);

        //triggering = false;
        controllerNPC.alive = false;

        //Count for star
        if ("Businessman" == triggeringNPC[NPC].transform.parent.name)
        {
            starpoint += 1;
        }
        if ("SWAT Officer" == triggeringNPC[NPC].transform.parent.name)
        {
            starpoint_hard += 1;
        }
        // Add Points
        if (objective == triggeringNPC[NPC].transform.parent.name){
            score += 200;
            // Define new objective
            objective = Objectives[Random.Range(0, Objectives.Count)];
        } else {
            score += 100;
        }

        triggering = false;
    }
}
