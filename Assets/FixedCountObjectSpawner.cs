using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixedCountObjectSpawner : MonoBehaviour
{
    public int targetCount;
    public GameObject spawnObject;

    private List<SpawnPoint> spawnPoints;

    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag(spawnObject.tag).Length < targetCount) {
            var valid = spawnPoints.Where(point => point.CanSpawn(spawnObject)).ToList();

            var chosen = valid[rnd.Next(valid.Count)];

            chosen.Spawn(spawnObject);
        }
    }

}
