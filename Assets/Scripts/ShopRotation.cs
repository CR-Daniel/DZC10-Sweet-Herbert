using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRotation : MonoBehaviour
{
    public GameObject IceCreamTruck1;
    public GameObject IceCreamTruck2;
    public GameObject IceCream1;
    public GameObject IceCream2;
    

    // Update is called once per frame
    void Update()
    {
        IceCreamTruck1.transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        IceCreamTruck2.transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        IceCream1.transform.Rotate(Vector3.up, 50f * Time.deltaTime);
        IceCream2.transform.Rotate(Vector3.up, 50f * Time.deltaTime);
    }
}
