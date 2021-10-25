using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    List<GameObject> children;
    void Start()
    {
        children = GetComponentsInChildren<Transform>().Select(t => t.gameObject).Where(g => g != gameObject).ToList();
    }

    public void SetDisplayValue(int value) {

        foreach (var child in children.Take(value)) {
            child.SetActive(true);
        }

        foreach (var child in children.Skip(value)) {
            child.SetActive(false);
        }
    }
}
