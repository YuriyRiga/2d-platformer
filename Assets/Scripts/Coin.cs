using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.2f; 
    [SerializeField] private float _frequency = 1f;
    [SerializeField] private float _timeLife = 5f;

    private Vector2 _startPosition;
    private ObjectPool<Coin> _objectPool;

    private void Update()
    {
        transform.position = _startPosition + Vector2.up * Mathf.Sin(Time.time * _frequency) * _amplitude;
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterTime());
    }

    private void OnDisable()
    {
        StopCoroutine(ReturnToPoolAfterTime());
    }

    public void SetPool(ObjectPool<Coin> objectPool)
    {
        _objectPool = objectPool;
    }

    public void SetPosition( Vector2 position)
    {
        _startPosition = position;
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);

        if (_objectPool != null)
        {
            _objectPool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            _objectPool.Release(this);
        }
    }
}
