using UnityEngine;
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
            Collider2D playerCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y + _offsetCircle), _attackRange, _playerLayer);

            if (playerCollider != null)
            {
                Player player = playerCollider.GetComponent<Player>();
                if (player != null && !player.IsDead)
                {
                    player.TakeDamage(_enemy.Damage);
                }
            }

            yield return delay;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + _offsetCircle), _attackRange);
    }
}
