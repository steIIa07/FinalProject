using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointBar : MonoBehaviour
{

    void Update()
    {
        // HPバーがカメラの方向を向くようにする / Make the HP bar face the camera
        transform.rotation = Camera.main.transform.rotation;
    }
}
