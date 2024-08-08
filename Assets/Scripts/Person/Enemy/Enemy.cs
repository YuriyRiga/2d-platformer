using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private Transform _spritePerson;

    private Health _health;

    public Transform SpritePerson => _spritePerson;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void Update()
    {
        if (_health.IsDead)
        {
            gameObject.SetActive(false);
        }
    }
}
