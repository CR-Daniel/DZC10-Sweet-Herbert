using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public double minDistance = 5.0;
    public bool CanSpawn(GameObject obj) {
        var existing = GameObject.FindGameObjectsWithTag(obj.tag);
        if (existing.Count() == 0) {
            return true;
        }

        var closest = existing.Select(o => Vector3.Distance(o.transform.position, transform.position))
                              .Min();
        
        return closest > minDistance;
    }

    public void Spawn(GameObject obj) {
        Instantiate(obj, transform.position, transform.rotation);
    }
}
