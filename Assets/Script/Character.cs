using System;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    protected int _health;
    public int Health { get { return _health; } set { _health = value; } }  
    [SerializeField] protected int _attackValue = 10;
    public int AttackValue { get { return _attackValue; } set { _attackValue = value; } }
    [SerializeField] protected int _maxHealth = 100;
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    [SerializeField] protected float _speed = 0.01f;
    [SerializeField] protected float _invincibleDuration;
    [SerializeField] protected AudioSource _hitSound;
    protected float _invincibleTime = 1.0f;
    protected bool _invincible = false;

    // ダメージを受ける / Take damage
    public void TakeDamage(int damage) {
        _hitSound.Play();
        _health -= damage;
        _health = Math.Max(0, _health);
    }

    // 死亡 / Death
    abstract public void Die();

    // ダメージをランダムに変動 / Randomly change the damage
    protected int Randomize(int value) {
        value += UnityEngine.Random.Range(-5, 6);
        if(UnityEngine.Random.Range(0, 5) == 0) {
            value *= 2;  // クリティカルヒット / Critical hit
        }
        return value;
    }
}
