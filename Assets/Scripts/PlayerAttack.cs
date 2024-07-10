using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private Animator _animator;

    private Player _player;
    private Enemy _enemyInRange;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (_enemyInRange != null && Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
            _enemyInRange.TakeDamage(_player.Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy _))
        {
            _enemyInRange = collision.GetComponent<Enemy>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy _))
        {
            _enemyInRange = null;
        }
    }
}
