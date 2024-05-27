using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static int _hitAnimationHash;
    private static int _walkAnimationHash;
    private static int _attackAnimationHash;
    private static int _deathAnimationHash;
    private Animator _animator;
    private Transform _player;
    private bool _isAttacking = false;
    private bool _isWalking = false;
    [SerializeField, Range(0f, 1000f)] private int _health = 100;  
    [SerializeField, Range(0f, 100f)] private int _damage = 10;
    [SerializeField] private float _speed = 0.01f;
    [SerializeField] private float _RotationSpeed = 0.03f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _awareRange = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        _hitAnimationHash = Animator.StringToHash("Hit");
        _walkAnimationHash = Animator.StringToHash("Walk");
        _attackAnimationHash = Animator.StringToHash("Attack");
        _deathAnimationHash = Animator.StringToHash("Death");
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = (transform.position-_player.position).sqrMagnitude;
        
        // 攻撃範囲内 / In attack range
        if(distance < _attackRange * _attackRange) {  
            _isAttacking = true;
            _isWalking = false;
        }
        // 警戒範囲内 / In alert range
        else if (_attackRange * _attackRange < distance && distance < _awareRange * _awareRange) {
            _isWalking = true;
            _isAttacking = false;
        }
        // 警戒範囲外 / Out of alert range
        else {
            _isWalking = false;
            _isAttacking = false;
        }

        // プレイヤーの方向を向く＆プレイヤーに向かって進む / Look at the player and move towards the player
        if(_isWalking && !_isAttacking) {
            var direction = _player.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _RotationSpeed * Time.deltaTime);
            transform.Translate(0, 0, _speed * Time.deltaTime);
            // 歩くアニメーション / Walking animation
        }
    }

    private void OnTriggerEnter(Collider other) {
        // プレイヤーの攻撃を受けた / Hit by the player's attack
        if (other.gameObject.tag == "Weapon") {
            Debug.Log("Hit");
            // ダメージを受ける / Take damage
        }
    }
}
