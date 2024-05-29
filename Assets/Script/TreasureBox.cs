using System.Collections;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private Animator _animator;
    static readonly int Open = Animator.StringToHash("Open");
    private void Start() {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            _animator.SetTrigger(Open);
            Destroy(gameObject, 10f);
        }
    }
}
