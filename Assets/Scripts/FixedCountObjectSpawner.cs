using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixedCountObjectSpawner : MonoBehaviour
{
    public int targetCount;
    public GameObject spawnObject;
    public double globalCooldown = 20;

    private List<SpawnPoint> spawnPoints;

    private double lastSpawn = double.NegativeInfinity;

    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawn + globalCooldown < Time.timeAsDouble)
            return;

        if (GameObject.FindGameObjectsWithTag(spawnObject.tag).Length < targetCount)
        {
            var valid = spawnPoints.Where(point => point.CanSpawn(spawnObject)).ToList();

            if (valid.Count == 0)
                return;

            var chosen = valid[rnd.Next(valid.Count)];

            chosen.Spawn(spawnObject);
            lastSpawn = Time.timeAsDouble;
        }
    }

}
