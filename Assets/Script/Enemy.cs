using System;
using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    private static int _hitAnimationHash = Animator.StringToHash("Hit");
    private static int _walkAnimationHash = Animator.StringToHash("Walk");
    private static int _attackAnimationHash = Animator.StringToHash("Attack");
    private static int _deathAnimationHash = Animator.StringToHash("Death");
    private Transform _player;
    private int _playerAttackValue;
    private bool _isAttacking = false;
    private bool _isWalking = false;
    private bool _isDead = false;
    private GameObject _sceneLoader = null;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _RotationSpeed = 0.03f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _awareRange = 10f;
    [SerializeField] private BoxCollider _attackCollider;
    [SerializeField] private AudioSource _deathSound;

    private void Awake() {
        _health = _maxHealth;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(name == "Boss"){
            _sceneLoader = GameObject.Find("SceneLoader");
            _sceneLoader?.SetActive(false);
        }
        var tmpObj = GameObject.FindGameObjectWithTag("Player");
        _player = tmpObj.transform;
        _playerAttackValue = tmpObj.GetComponent<Player>().AttackValue;
        _attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead) return;

        if(_invincible) {
            _invincibleTime -= Time.deltaTime;
            if(_invincibleTime <= 0) {
                _invincible = false;
                _invincibleTime = _invincibleDuration;
            }
        }

        // プレイヤーとの距離を計算 / Calculate the distance between the player
        var distance = Vector3.SqrMagnitude(_player.position - transform.position);
        
        // 攻撃範囲内 / In attack range
        if(distance < _attackRange * _attackRange) {  
            _isAttacking = true;
            _isWalking = false;
            _animator.SetTrigger(_attackAnimationHash);
        }
        // 警戒範囲内 / In alert range
        else if (_attackRange * _attackRange < distance && distance < _awareRange * _awareRange) {
            _isWalking = true;
            _isAttacking = false;
            _animator.SetBool(_walkAnimationHash, true);
        }
        // 警戒範囲外 / Out of alert range
        else {
            _isWalking = false;
            _isAttacking = false;
            _animator.SetBool(_walkAnimationHash, false);
        }

        // プレイヤーの方向を向く＆プレイヤーに向かって進む / Look at the player and move towards the player
        if(_isWalking && !_isAttacking) MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        var direction = _player.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _RotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Weapon") {
            if(_invincible) return;
            _invincible = true;
            TakeDamage(Randomize(_playerAttackValue));
            if(_health == 0) Die();
            _animator.SetTrigger(_hitAnimationHash);
        }
    }

    public override void Die()
    {
        _isDead = true;
        _animator.SetTrigger(_deathAnimationHash);
        _deathSound.Play();
        Destroy(gameObject, 3.0f);
    }

    public void Attack()
    {
        _attackCollider.enabled = true;
    }

    public void StopAttack()
    {
        _attackCollider.enabled = false;
    }

    private void OnDestroy() {
        if(name == "Boss") _sceneLoader?.SetActive(true);
    }
}
