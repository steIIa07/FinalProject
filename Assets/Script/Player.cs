using UnityEngine;

public class Player : Character
{
    // Update is called once per frame
    void Update()
    {
        Vector2 stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);;  // スティック入力 / Stick input
        Vector3 movement = new Vector3(stickInput.x, 0, stickInput.y);
        var cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 pos = cameraForward * movement.z + Camera.main.transform.right * movement.x;
        transform.position += pos * _speed * Time.deltaTime;
    }

    public override void Die()
    {
    }
}
