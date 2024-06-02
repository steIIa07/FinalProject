using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : Character
{
    private void Awake() {
        _health = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(_invincible) {
            _invincibleTime -= Time.deltaTime;
            if(_invincibleTime <= 0) {
                _invincible = false;
                _invincibleTime = _invincibleDuration;
            }
        }

        Vector2 stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);;  // スティック入力 / Stick input
        Vector3 movement = new Vector3(stickInput.x, 0, stickInput.y);
        var cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 pos = cameraForward * movement.z + Camera.main.transform.right * movement.x;
        if(OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger)) {
            pos *= 1.5f;
        }
        transform.position += pos * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            if(_invincible) return;
            _invincible = true;
            TakeDamage(Randomize(other.GetComponentInParent<Enemy>().AttackValue));
            if(_health == 0) Die();
        }
    }

    public void Heal(int value) {
        _health += value;
        _health = Mathf.Min(_health, _maxHealth);
    }

    public override void Die()
    {
        SceneManager.LoadScene("Game");
    }
}
