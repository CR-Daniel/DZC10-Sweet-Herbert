using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public double minDistance = 5.0;
    public double cooldown = 40;
    private double lastUsed = double.NegativeInfinity;

    public bool CanSpawn(GameObject obj)
    {
        if (lastUsed + cooldown >= Time.timeAsDouble)
            return false;

        var existing = GameObject.FindGameObjectsWithTag(obj.tag);
        if (existing.Count() == 0)
            return true;

        var closest = existing.Select(o => Vector3.Distance(o.transform.position, transform.position))
                              .Min();

        return closest > minDistance;
    }

    public void Spawn(GameObject obj)
    {
        var newObj = Instantiate(obj, transform.position, transform.rotation);
        newObj.SetActive(true);
        lastUsed = Time.timeAsDouble;
    }
}
