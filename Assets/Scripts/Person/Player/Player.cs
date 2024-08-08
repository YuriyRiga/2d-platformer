using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Health _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health.IsDead)
        {
            gameObject.GetComponent<PlayerMover>().enabled = false;
            _animator.SetTrigger(PlayerAnimatorData.Params.Death);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Deactivate();
        }

        if (collision.gameObject.TryGetComponent(out Heal heal))
        {
            _health.TakeHeal(heal.Value);
            heal.Deactivate();
        }
    }
}
