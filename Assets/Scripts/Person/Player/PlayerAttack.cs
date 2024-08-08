using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _enemyLayer;

    private Player _player;
    private VampireAttack _vampireAttack;
    private Attack _attack;
    private Enemy _enemyInRange;
    private bool _isSuckBlood = false;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _vampireAttack = GetComponent<VampireAttack>();
        _attack = GetComponent<Attack>();
    }

    private void Update()
    {
        if (_enemyInRange != null && Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
            AttackSword();
        }

        if (Input.GetMouseButton(1))
        {
            SuckBloodAttack();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopSuckBloodAttack();
        }
    }

    private void SuckBloodAttack()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + _vampireAttack.OffsetCircle), _vampireAttack.AttackRange, _enemyLayer);

        if (enemyColliders.Length > 0)
        {
            Collider2D nearestEnemyCollider = enemyColliders[0];
            float minDistanceSq = (transform.position - nearestEnemyCollider.transform.position).sqrMagnitude;

            for (int i = 1; i < enemyColliders.Length; i++)
            {
                float distanceSq = (transform.position - enemyColliders[i].transform.position).sqrMagnitude;

                if (distanceSq < minDistanceSq)
                {
                    minDistanceSq = distanceSq;
                    nearestEnemyCollider = enemyColliders[i];
                }
            }

            _vampireAttack.StartAttack(nearestEnemyCollider.GetComponent<Health>());
        }
    }

    private void StopSuckBloodAttack()
    {
        _vampireAttack.StopAttack();
    }

    private void AttackSword()
    {
        _enemyInRange.GetComponent<Health>().TakeDamage(_attack.Damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            _enemyInRange = collision.GetComponent<Enemy>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            _enemyInRange = null;
        }
    }
}
