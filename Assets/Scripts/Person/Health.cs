using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _maxhealth = 100f;

    public event Action<float, float> ChangeHealth;

    private bool _isDead = false;

    public bool IsDead => _isDead;

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        if(_currentHealth == 0) 
        {
            _isDead = true;
        }

        ChangeHealth.Invoke(_currentHealth, _maxhealth);
    }

    public void TakeHeal(float heal)
    {
        if (heal < 0) return;

        _currentHealth = Mathf.Min(_currentHealth + heal, _maxhealth);

        ChangeHealth.Invoke(_currentHealth, _maxhealth);
    }
}
