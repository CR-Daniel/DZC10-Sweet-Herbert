using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public double minDistance = 5;
    public double cooldown = 40;
    public GameObject awayFromTarget;
    public double minDistanceFromTarget = 15;
    private double lastUsed = double.NegativeInfinity;

    public bool CanSpawn(GameObject obj)
    {
        if (lastUsed + cooldown >= Time.timeAsDouble)
            return false;

        var existing = GameObject.FindGameObjectsWithTag(obj.tag);
        float closest;
        if (existing.Count() == 0)
        {
            closest = float.PositiveInfinity;
        }
        else
        {
            closest = existing.Select(o => Vector3.Distance(o.transform.position, transform.position))
                                  .Min();
        }
        var targetDistance = Vector3.Distance(transform.position, awayFromTarget.transform.position);

        return closest > minDistance && targetDistance > minDistanceFromTarget;
    }

    public void Spawn(GameObject obj)
    {
        var newObj = Instantiate(obj, transform.position, transform.rotation);
        newObj.SetActive(true);
        lastUsed = Time.timeAsDouble;
    }
}
