using UnityEngine;
using UnityEngine.UI;

public class HitPointBar : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Character _character;

    private void Start() {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _character.MaxHealth;
        _slider.value = _character.MaxHealth;
    }
    void Update()
    {
        // HPバーがカメラの方向を向くようにする / Make the HP bar face the camera
        var dir = Camera.main.transform.rotation;
        dir.z = 0;
        transform.rotation = dir;

        // HPバーの値を敵のHPに合わせる / Set the value of the HP bar to match the _enemy's HP
        _slider.value = _character.Health;
    }
}
