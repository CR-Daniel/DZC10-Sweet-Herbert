using System.Collections;
using System.Collections.Generic;
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

    private void Explode() {
        AudioClip clipsExplode = GetExplodeClip();
        audioSource.PlayOneShot(clipsExplode);
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
        agent.isStopped = true;

        
        initialAggroTrigger = CreateCollider(initialAggroRange);

        initialAggroTrigger.Enter += (collider) =>
        {
            if (collider.gameObject == target)
            {
                //alert effect
                Alert();
                agent.isStopped = false;
                alert.SetActive(true);
            }
        };

        maintainAggroTrigger = CreateCollider(maintainAggroRange);

        maintainAggroTrigger.Exit += (collider) =>
        {
            if (collider.gameObject == target)
            {
                //TODO lost effect
                agent.isStopped = true;
                alert.SetActive(false);
            }
        };

        
        detonateTrigger = CreateCollider(detonateRange);

        detonateTrigger.Enter += (collider) =>
        {
            if (collider.gameObject == target && !hasDetonated)
            {
                //TODO explosion sound
                Explode();
                SpawnExplosion();
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
