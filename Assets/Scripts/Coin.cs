using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.5f; 
    [SerializeField] private float _frequency = 1f;   

    private Vector2 _startPosition;

    public void SetPosition( Vector2 position)
    {
        _startPosition = position;
    }

    private void Update()
    {
        transform.position = _startPosition + Vector2.up * Mathf.Sin(Time.time * _frequency) * _amplitude;
    }
}
