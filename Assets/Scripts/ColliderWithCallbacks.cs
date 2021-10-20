using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ColliderWithCallbacks : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action<Collider> Enter;
    public event Action<Collider> Stay;
    public event Action<Collider> Exit;

    void OnTriggerEnter(Collider other) => Enter(other);

    void OnTriggerExit(Collider other) => Exit(other);

    void OnTriggerStay(Collider other) => Stay(other);

    public void RemoveListeners()
    {
        Enter = null;
        Exit = null;
        Stay = null;
    }
}
