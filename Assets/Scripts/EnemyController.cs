using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public GameObject target, explosion;
    public int initialAggroRange, maintainAggroRange, detonateRange;

    public GameObject alert;

    private NavMeshAgent agent;

    private bool hasDetonated = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        var initialAggroTrigger = CreateCollider(initialAggroRange);
        initialAggroTrigger.Enter += (collider) =>
        {
            if (collider.gameObject == target)
            {
                //TODO spotted effect
                agent.isStopped = false;
                alert.SetActive(true);
            }
        };

        var maintainAggroTrigger = CreateCollider(maintainAggroRange);
        maintainAggroTrigger.Exit += (collider) =>
        {
            if (collider.gameObject == target)
            {
                //TODO lost effect
                agent.isStopped = true;
                alert.SetActive(false);
            }
        };

        var detonateTrigger = CreateCollider(detonateRange);
        detonateTrigger.Enter += (collider) =>
        {
            if (collider.gameObject == target && !hasDetonated)
            {
                //TODO explosion sound
                SpawnExplosion();
                target.GetComponent<Health>()?.Damage(1);

                Destroy(gameObject);
                hasDetonated = true;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    private void SpawnExplosion()
    {
        var exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.SetActive(true);
    }

    private ColliderWithCallbacks CreateCollider(int size)
    {
        var mainCollider = GetComponent<BoxCollider>();
        var obj = new GameObject();
        obj.transform.SetParent(gameObject.transform, false);

        var collider = obj.AddComponent<BoxCollider>();
        collider.center = new Vector3(0, mainCollider.center.y, 0);
        collider.size = new Vector3(size, mainCollider.size.y, size);

        collider.isTrigger = true;

        return obj.AddComponent<ColliderWithCallbacks>();
    }
}
