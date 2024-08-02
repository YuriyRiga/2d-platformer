﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _offsetCircle = 1f;
    [SerializeField] private LayerMask _playerLayer;

    private Enemy _enemy;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _attackCoroutine = StartCoroutine(AttackPlayer());
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    private IEnumerator AttackPlayer()
    {
        var delay = new WaitForSeconds(_attackCooldown);

        while (true)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + _offsetCircle), _attackRange, _playerLayer);

            if (playerCollider != null)
            {
                if (playerCollider.TryGetComponent(out Player player) && player.IsDead == false)
                {
                    player.TakeDamage(_enemy.Damage);
                }
            }

            yield return delay;
        }
    }
}