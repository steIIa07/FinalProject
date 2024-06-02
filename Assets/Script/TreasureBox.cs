using System.Collections;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private float power = 3;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ParticleSystem[] particles = new ParticleSystem[2];
    static readonly int Open = Animator.StringToHash("Open");
    private void Start() {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            _animator.SetTrigger(Open);
            _audioSource.Play();
            GetComponent<BoxCollider>().enabled = false;
            foreach (var particle in particles) {
                particle.Play();
            }
            GameObject obj = Instantiate(prefab, transform.position, prefab.transform.rotation);
            var dir = -transform.right + transform.up;
            obj.GetComponent<Rigidbody>().AddForce(dir * power, ForceMode.Impulse);
        }
    }
}
