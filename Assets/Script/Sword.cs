using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider _collider;
    private Vector3 _previousPosition;
    [SerializeField] private float _threshold = 3f; // エディターからいじれる閾値 / Threshold that can be adjusted from the editor

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 速度を計算 / Calculate speed
        var currentPosition = transform.position;
        var distance = Vector3.Distance(_previousPosition, currentPosition);
        var speed = distance / Time.deltaTime;

        // Debug.Log(speed);
        
        // 速度が閾値を超えたら(右手動かしてる)コライダーを有効にする / Enable the collider if the speed exceeds the threshold
        if (speed > _threshold)
        {
            _collider.enabled = true;
        }
        else
        {
            _collider.enabled = false;
        }

        _previousPosition = currentPosition; // 位置を更新 / Update position
    }
}
