using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampireAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 3.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _timerAttack = 6f;
    [SerializeField] private Image _image;

    private float _currentTimer;
    private float _offsetCircle = 1f;
    private bool _isWork = false;
    private float _targetFillValue = 1;
    private Health _enemyHealth = null;
    private Coroutine _fillBarCoroutine;
    private Coroutine _attackCoroutine;

    public float AttackRange => _attackRange;
    public float OffsetCircle => _offsetCircle;

    private void Start()
    {
        _currentTimer = _timerAttack;
        _image.transform.localScale = Vector3.one * _attackRange;
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
        }

        _fillBarCoroutine = StartCoroutine(FillBar());
    }

    public void StartAttack(Health health)
    {
        _enemyHealth = health;

        if (_isWork == false && _currentTimer == _timerAttack)
        {
            _isWork = true;
            _attackCoroutine =  StartCoroutine(Attack());
        }

        if (health.IsDead)
        {
            StopAttack();
        }
    }

    public void StopAttack()
    {
        _enemyHealth = null;

        if (_isWork)
        {
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
            }

            yield return delay;
        }
    }

    private IEnumerator FillBar()
    {
        _targetFillValue = _currentTimer / _timerAttack;

        while (_image.fillAmount != _targetFillValue)
        {
            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, _targetFillValue, Time.deltaTime);
            yield return null;
        }

        _fillBarCoroutine = null;
    }
}
