using System.Collections;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    static readonly int Open = Animator.StringToHash("Open");
    private void Start() {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            _animator.SetTrigger(Open);
            _audioSource.Play();
            Destroy(gameObject, 5f);
        }
    }
}
