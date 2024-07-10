using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxhealth = 100f;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private Animator _animator;

    private bool _isDead = false;
    public float Damage => _damage;
    public bool IsDead => _isDead;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            _animator.SetTrigger(PlayerAnimatorData.Params.Death);
            gameObject.GetComponent<PlayerMover>().enabled = false;
            _isDead = true;
        }
    }

    private void TakeHeal(float heal)
    {
        _health += heal;

        if (_health >= _maxhealth)
        {
            _health = _maxhealth;
        }
    }

    private void Update()
    {
        Debug.Log(_health);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Deactivate();
        }

        if (collision.gameObject.TryGetComponent(out Heal heal))
        {
            TakeHeal(heal.Value);
            heal.Deactivate();
        }
    }
}
