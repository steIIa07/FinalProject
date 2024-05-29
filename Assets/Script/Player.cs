using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Vector2 movement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);  // スティック入力 / Stick input
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));  // テスト用 / For testing
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
