using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class EnemyController : MonoBehaviour
{

    public GameObject target, explosion;
    public int initialAggroRange, maintainAggroRange, detonateRange;
    public ColliderWithCallbacks initialAggroTrigger, maintainAggroTrigger, detonateTrigger;

    public GameObject alert;

    private NavMeshAgent agent;

    private bool hasDetonated = false;

    [SerializeField]
    private AudioSource audioSource;
    private AudioClip[] clipsAlert;

    private AudioClip[] clipsExplode;

    private GameObject currentTarget;

    public GameObject spawnPoints;

    // Sounds - start
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Alert() {
        AudioClip clipAlert = GetAlertClip();
        audioSource.PlayOneShot(clipAlert); 
    }

    private AudioClip GetAlertClip(){
        clipsAlert = new AudioClip[]{
            (AudioClip)Resources.Load("Alert/MetalGear"),
            (AudioClip)Resources.Load("Alert/EnemySpotted")
        };
        return clipsAlert[UnityEngine.Random.Range(0, clipsAlert.Length)];
    }

    private void Explode(AudioSource source) {
        AudioClip clipsExplode = GetExplodeClip();
        source.PlayOneShot(clipsExplode);
    }

    private AudioClip GetExplodeClip() {
        clipsExplode = new AudioClip[]{
            (AudioClip)Resources.Load("Explosion/Explode")
        };
        return clipsExplode[UnityEngine.Random.Range(0, clipsExplode.Length)];
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTarget = GetClosestSpawnPoint();
        
        initialAggroTrigger = CreateCollider(initialAggroRange);

        initialAggroTrigger.Enter += (collider) =>
        {
            if (collider.gameObject == target)
            {
                //alert effect
                Alert();
                currentTarget = target;
                alert.SetActive(true);
            }
        };

        maintainAggroTrigger = CreateCollider(maintainAggroRange);

        maintainAggroTrigger.Exit += (collider) =>
        {
            if (collider.gameObject == target)
            {
                //TODO lost effect
                currentTarget = GetClosestSpawnPoint();
                alert.SetActive(false);
            }
        };

        
        detonateTrigger = CreateCollider(detonateRange);

        detonateTrigger.Enter += (collider) =>
        {
            if (collider.gameObject == target && !hasDetonated)
            {
                var explosion = SpawnExplosion();
                Explode(explosion.GetComponent<AudioSource>());
                target.GetComponent<Health>()?.Damage(1);

                DestroyObject();
                hasDetonated = true;
            }
        };
    }

    void DestroyObject() {
        Destroy(gameObject);

        initialAggroTrigger.RemoveListeners();
        maintainAggroTrigger.RemoveListeners();
        detonateTrigger.RemoveListeners();

        Destroy(initialAggroTrigger);
        Destroy(maintainAggroTrigger);
        Destroy(detonateTrigger);

        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(currentTarget.transform.position);
    }

    private GameObject SpawnExplosion()
    {
        var exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.SetActive(true);
        return exp;
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

    private GameObject GetClosestSpawnPoint() {
        var spawns = spawnPoints.GetComponentsInChildren<SpawnPoint>().Select(x => x.gameObject).OrderBy(x => Vector3.Distance(x.transform.position, transform.position));
        return spawns.First();
    }
}
