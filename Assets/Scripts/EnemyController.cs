using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum Mode
    {
        IDLE, CHASING
    }

    public GameObject target;
    public int initialAggroRange, maintainAggroRange, detonateRange;

    private NavMeshAgent agent;

    private Mode currentMode = Mode.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);

        if (currentMode == Mode.IDLE && distance <= initialAggroRange)
        {
            //TODO popup exclamation
            currentMode = Mode.CHASING;
        }

        if (currentMode == Mode.CHASING && distance > maintainAggroRange)
        {
            //TODO popup question mark
            currentMode = Mode.IDLE;
        }

        if (distance <= detonateRange) {
            //TODO explosion
            Destroy(gameObject);
            Destroy(target);
            return;
        }

        UpdateAgent();
    }

    private void UpdateAgent() {
        if (currentMode == Mode.IDLE) {
            //TODO find an idling spot?
            agent.isStopped = true;
        } else {
            agent.SetDestination(target.transform.position);
            agent.isStopped = false;
        }
    }

}
