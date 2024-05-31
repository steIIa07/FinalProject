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
    
    protected void Awake() {
        _health = _maxHealth;
        Debug.Log(name + ": HP = " + _health);
    }

    // ダメージを受ける / Take damage
    public void TakeDamage(int damage) {
        _health -= damage;
        _health = Math.Max(0, _health);
        transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward, 1f);
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
