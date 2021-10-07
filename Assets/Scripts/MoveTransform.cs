// https://bit.ly/3ogOumw
// https://answers.unity.com/questions/1307105/how-can-i-make-my-object-go-back-to-its-start-posi.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    private Vector3 start;
    [SerializeField] private Vector3 target = new Vector3(1, 1, 1);
    private int lap = 0;
    [SerializeField] private float speed = 1;

    private void Start()
    {
        start = this.transform.position;
    }

    private void Update()
    {
        if (transform.position == target | transform.position == start)
        {
            lap++;
        }

        if (Player.triggering)
        {
            // Makes the object come to a stand still
            return;
        }
        else if (lap % 2 == 0)
        {
            // Moves the object to target position
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
        else
        {
            // Moves the object back to start position
            transform.position = Vector3.MoveTowards(transform.position, start, Time.deltaTime * speed);
        }
    }
}