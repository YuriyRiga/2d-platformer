using System;
using System.Collections;
using UnityEngine;

public class VampireAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 3.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _timerAttack = 6f;

    private float _currentTimer;
    private float _offsetCircle = 1f;
    private bool _isWork = false;
    private Health _enemyHealth = null;
    private Coroutine _attackCoroutine;

    public event Action<float, float> ChangeTimer;

    public float AttackRange => _attackRange;
    public float OffsetCircle => _offsetCircle;

    private void Start()
    {
        _currentTimer = _timerAttack;
    }

    private void Update()
    {
        if (_currentTimer <= 0)
        {
            StopAttack();
        }

        if (_isWork == false && _currentTimer < _timerAttack)
        {
            _currentTimer += Time.deltaTime;
            _currentTimer = Mathf.Min(_currentTimer, _timerAttack);
            ChangeTimer.Invoke(_currentTimer, _timerAttack);
        }
    }

    public void StartAttack(Transform transform)
    {
        _enemyHealth = transform.GetComponent<Health>();
        
        if (_enemyHealth.IsDead)
        {
            StopAttack();
            return;
        }

        if (_isWork == false && _currentTimer == _timerAttack)
        {
            _isWork = true;
            _attackCoroutine =  StartCoroutine(Attack());
        }
    }

    public void StopAttack()
    {
        _enemyHealth = null;

        if (_isWork)
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            _attackCoroutine = null;
            _isWork = false;
        }
    }

    private IEnumerator Attack()
    {
        var delay = new WaitForSeconds(_attackCooldown);

        float stoptimer = 1;

        while (_currentTimer >= stoptimer && _isWork)
        {
            if (_enemyHealth != null)
            {
                _enemyHealth.TakeDamage(_damage);
                _currentTimer -= _attackCooldown;
                ChangeTimer.Invoke(_currentTimer, _timerAttack);
            }

            yield return delay;
        }
    }
}
