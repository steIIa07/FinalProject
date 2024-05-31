using System;
using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    private static int _hitAnimationHash = Animator.StringToHash("Hit");
    private static int _walkAnimationHash = Animator.StringToHash("Walk");
    private static int _attackAnimationHash = Animator.StringToHash("Attack");
    private static int _deathAnimationHash = Animator.StringToHash("Death");
    private Animator _animator;
    private Transform _player;
    private bool _isAttacking = false;
    private bool _isWalking = false;
    private bool _isDead = false;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _RotationSpeed = 0.03f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _awareRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead) return;

        // プレイヤーとの距離を計算 / Calculate the distance between the player
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
        if(_isWalking && !_isAttacking) MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        var direction = _player.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _RotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Weapon") {
            TakeDamage(Randomize(_damage));
            if(_health == 0) Die();
        }
    }

    public override void Die()
    {
        _isDead = true;
        // _animator.SetTrigger(_deathAnimationHash);
        Destroy(gameObject, 3.0f);
    }
}
