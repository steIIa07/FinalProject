using UnityEngine;

public class RecoveryItem : MonoBehaviour
{
    [SerializeField] private int _healValue = 30;

   private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Heal(_healValue);
            Destroy(gameObject);
        }
   } 
}
