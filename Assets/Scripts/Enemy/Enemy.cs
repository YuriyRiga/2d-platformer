using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _damage = 15f;

    public float Damage => _damage;

    private void Update()
    {
        Debug.Log("Враг" + _health);

        if (_health < 0)
        {
            _health = 0;
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
