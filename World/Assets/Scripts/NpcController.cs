using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    private GameObject player;
    private Transform[] Waypoints;

    public float minDistance = 5;
    public float rotationSpeed = 10f;

    private int index = 0;
    public bool alive = true;

    private Vector3 ogPosition;
    private Quaternion ogRotation;

    void Start()
    {
        ogPosition = transform.position;
        ogRotation = transform.rotation;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        Waypoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < Waypoints.Length; i++)
        {
            Waypoints[i] = PATH.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (!alive)
        {
            agent.isStopped = true;

            // if distance between NPC and PLAYER > 50
            if (Vector3.Distance(transform.position, GameObject.Find("RedCar").transform.position) > 20f)
            {
                respawn();
            }
        }
        else if (!Player.triggering)
        {
            // Allow Motion
            agent.isStopped = false;
            roam();
        }
        else
        {
            // Stop Motion
            agent.isStopped = true;

            // Idle
            animator.SetFloat("vertical", 0);

            // Rotate Agent towards Player
            player = GameObject.Find("RedCar");
            RotateTowards(player.transform);
        }
    }

    private void roam()
    {
        if (Vector3.Distance(transform.position, Waypoints[index].position) < minDistance)
        {
            if (index + 1 != Waypoints.Length)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }

        agent.SetDestination(Waypoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }

    // https://answers.unity.com/questions/540120/how-do-you-update-navmesh-rotation-after-stopping.html
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void respawn()
    {
        animator.SetBool("Death_01", false);
        animator.SetBool("Death_02", false);
        animator.SetBool("Death_03", false);
        animator.SetBool("respawn", true);

        alive = true;
        
        GameObject.Find("Ch03").GetComponent<SkinnedMeshRenderer>().enabled = true;

        agent.isStopped = false;

        transform.position = ogPosition;
        transform.rotation = ogRotation;

        //animator.SetBool("respawn", false);
    }
}