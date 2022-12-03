using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.z > 7)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (transform.position.z < -7)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
