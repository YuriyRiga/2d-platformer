using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private DetectingEnemy _detectingEnemy;

    private VampireAttack _vampireAttack;
    private Attack _attack;
    private Enemy _enemyInRange;

    private void Awake()
    {
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

        if (Input.GetMouseButton(1) && _detectingEnemy.GetClosestEnemy() != false)
        {
            _vampireAttack.StartAttack(_detectingEnemy.GetClosestEnemy());
        }
        else
        {
            _vampireAttack.StopAttack();
        }
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
