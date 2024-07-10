using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;

    private Enemy _enemy;
    private bool _isPlayerInRange = false;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            _isPlayerInRange = true;

            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(AttackCooldown(collision.GetComponent<Player>()));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            _isPlayerInRange = false;

            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackCooldown(Player player)
    {
        var delay = new WaitForSeconds(_attackCooldown);

        while (_isPlayerInRange && player.IsDead == false)
        {
            player.TakeDamage(_enemy.Damage);
            yield return delay;
        }
    }
}
