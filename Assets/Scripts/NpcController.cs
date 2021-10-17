using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    private GameObject visual;

    public GameObject PATH;
    private GameObject player;
    private Transform[] Waypoints;

    public float minDistance = 5;
    public float rotationSpeed = 10f;

    private int index = 0;
    private float min = 100;
    public bool alive = true;

    private Vector3 ogPosition;
    private Quaternion ogRotation;
    private bool direction;

    private GameObject iceCream;

    void Start()
    {
        iceCream = GameObject.Find(gameObject.name + "/iceCream");
        iceCream.SetActive(false);

        ogPosition = transform.position;
        ogRotation = transform.rotation;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        visual = GameObject.Find(gameObject.name + "/visual");

        Waypoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < Waypoints.Length; i++)
        {
            Waypoints[i] = PATH.transform.GetChild(i);
        }

        // Set Index to Closest Waypoint
        for (int i = 0; i < Waypoints.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, Waypoints[i].position);

            if (dist < min){
                min = dist;
                index = i;
            }
        }

        // Up/Down Direction
        direction = Random.value > 0.5f;

        // Walking Speed (min 2 max 5)
        GetComponent<NavMeshAgent>().speed = Random.Range(2, 6);;
        
    }

    void Update()
    {
        // Get Player By Tag
        player =  GameObject.FindWithTag("Player");

        if (!alive)
        {
            agent.isStopped = true;
            iceCream.SetActive(false);

            // if distance between NPC and PLAYER > 50
            if (Vector3.Distance(transform.position, player.transform.position) > 20f)
            {
                respawn();
            }
        }
        else if (!Player.triggeringNPC.ContainsKey(gameObject.name))
        {
            // Keep Ice Cream Hidden
            iceCream.SetActive(false);

            // Allow Motion
            agent.isStopped = false;
            roam();
        }
        else
        {
            // Stop Motion
            agent.isStopped = true;

            // IceCream Icon + Spin
            iceCream.SetActive(true);
            iceCream.transform.Rotate(Vector3.up, 50f * Time.deltaTime);

            // Idle
            animator.SetFloat("vertical", 0);

            // Rotate Agent towards Player
            RotateTowards(player.transform);
        }
    }

    private void roam()
    {
        if (direction)
        {
            if (Vector3.Distance(transform.position, Waypoints[index].position) < minDistance)
            {
                if (index + 1 != Waypoints.Length)
                {
                    index++;
                } else {
                    index = 0;
                }      
            }
        } else {
            if (Vector3.Distance(transform.position, Waypoints[index].position) < minDistance)
            {
                if (index - 1 != -1)
                {
                    index--;
                } else {
                    index = Waypoints.Length - 1;
                }      
            }   
        }

        agent.SetDestination(Waypoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void respawn()
    {
        // prevent immediate transition to death animation
        animator.SetBool("Death_01", false);
        animator.SetBool("Death_02", false);
        animator.SetBool("Death_03", false);

        alive = true;
        agent.isStopped = false;

        transform.position = ogPosition;
        transform.rotation = ogRotation;

        // Put NPC back in Walking Animation
        animator.SetBool("respawn", true);

        // Make NPC Visible
        visual.SetActive(true);
    }
}